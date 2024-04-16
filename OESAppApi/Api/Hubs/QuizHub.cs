using Domain.Entities.Common;
using Domain.Entities.Courses;
using Domain.Entities.Questions;
using Domain.Entities.Quizzes;
using Domain.Entities.Tests.Answers;
using Domain.Entities.Users;
using Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using OESAppApi.Pages;
using Persistence;
using System.Data;

namespace OESAppApi.Api.Hubs;
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class QuizHub : Hub
{
    private readonly OESAppApiDbContext _context;
    private readonly InMemoryQuizService _inMemoryQuizService;
    private readonly ICourseRepository _courseRepository;
    private readonly IUserRepository _userRepository;

    public QuizHub(OESAppApiDbContext context, InMemoryQuizService inMemoryQuizService, IUserRepository userRepository, ICourseRepository courseRepository)
    {
        _context = context;
        _inMemoryQuizService = inMemoryQuizService;
        _courseRepository = courseRepository;
        _userRepository = userRepository;
    }

    public async Task JoinGroup(int userId, int quizId)
    {
        Quiz? quiz = await _context.Quiz
            .Where(q => q.Id == quizId)
            .SingleOrDefaultAsync();
        if (quiz is null)
        {
            await Clients.Caller.SendAsync("JoinGroupErrorCallback", $"Unable to join the group courseId! {quizId}");
            return;
        }

        var result = await _courseRepository.GetUserCourseRoleAsync(quiz.CourseId, userId);
        if (!result.IsSuccess)
        {
            await Clients.Caller.SendAsync("JoinGroupErrorCallback", "Unable to join the group userRole!");
            return;
        }

        string groupName = quizId.ToString();
        QuizUser toAdd = new(Context.ConnectionId, result.Value, [], 0);
        _inMemoryQuizService.AddUserToGroup(quizId, userId, toAdd);
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        List<int> userIds = _inMemoryQuizService
               .GetAllUsersWithoutTeachers(quizId)
               .Select(user => user.Key)
               .ToList();
        List<UserResponse> response = (await _userRepository
            .GetByCondition(u => userIds.Contains(u.Id)))
            .Select(u => new UserResponse(u.Id, u.FirstName, u.LastName, u.Username))
            .ToList();
        await Clients.Group(groupName).SendAsync("JoinGroupCallback", response, userId);
    }

    public async Task SubmitAnswer(int userId, int quizId, List<QuizAnswer> answers)
    {
        if (!_inMemoryQuizService.AddAnswers(quizId, userId, answers))
        {
            await Clients.Caller.SendAsync("SubmitAnswerErrorCallback", "Unable submit answers!!");
            return;
        }

        Question? q = await _context.Question
            .Where(q => q.Id == answers.First().QuestionId)
            .SingleOrDefaultAsync();

        if (q is null)
        {
            await Clients.Caller.SendAsync("SubmitAnswerErrorCallback", $"Unable to find the question (ID: {answers.First().QuestionId}) you submitted answers for!!");
            return;
        }

        var result = _inMemoryQuizService.GetTeacher(quizId);
        if (!result.IsSuccess)
            return;
        
        await Clients.Client(result.Value!.ConnectionId).SendAsync("SubmitAnswerCallback");
    }

    public async Task NextQuestion(int userId, int quizId, bool load)
    {
        if (load)
        {
            var questions = await _context.Question
                .Include(q => q.Options)
                .Where(q => q.ItemId == quizId)
                .ToListAsync();
            questions = questions.OrderBy(x => Random.Shared.Next()).ToList();
            _inMemoryQuizService.AddQuizQuestions(quizId, questions);
            _inMemoryQuizService.ResetUsers(quizId);
        }
            
        string groupName = quizId.ToString();
        var result = _inMemoryQuizService.NextQuizQuestion(quizId, userId);
        if (!result.IsSuccess)
        {
            await Clients.Group(groupName).SendAsync("QuizFinished");
            return;
        }

        await Clients.Group(groupName).SendAsync("NextQuestionCallback", result.Value);
    }

    public async Task ShowCurrentQuestionResults(int userId, int quizId)
    {
        string groupName = quizId.ToString();
        Result<CourseEnum> roleResult = _inMemoryQuizService.GetGroupUserRole(quizId, userId);
        if (!roleResult.IsSuccess || roleResult.Value is not CourseEnum.Teacher)
            return;

        Result<Question> questionResult = _inMemoryQuizService.PopQuizQuestion(quizId);
        if (!questionResult.IsSuccess)
        {
            await Clients.Caller.SendAsync("ShowCurrentQuestionResultsErrorCallback",
                $"No more questions for this quiz!");
            return;
        }

        Dictionary<int, QuizUser> usersWithoutTeachers = _inMemoryQuizService.GetAllUsersWithoutTeachers(quizId);
        Dictionary<int, int> userQuestionPoints = [];
        foreach (var user in usersWithoutTeachers)
        {
            userQuestionPoints[user.Key] = _inMemoryQuizService.CheckAnswer(questionResult.Value!, user.Key);
        }

        List<Task> tasks = [];
        foreach (var user in usersWithoutTeachers)
        {
            tasks.Add(Task.Run(async () =>
            {
                var position = _inMemoryQuizService.GetUserPositionForQuestion(quizId, questionResult.Value!, user.Key);
                await Clients.Client(user.Value.ConnectionId).SendAsync("ShowCurrentQuestionResultsCallback", userQuestionPoints[user.Key], position);
            }));
        }
        await Task.WhenAll(tasks);
    }

    public async Task ShowResults(int userId, int quizId)
    {
        string groupName = quizId.ToString();
        Result<CourseEnum> roleResult = _inMemoryQuizService.GetGroupUserRole(quizId, userId);
        if (!roleResult.IsSuccess || roleResult.Value is not CourseEnum.Teacher)
            return;

        var usersWithoutTeachers = _inMemoryQuizService.GetAllUsersWithoutTeachers(quizId);
        List<UserQuizResultResponse> response = (await _userRepository
            .GetByCondition(u => usersWithoutTeachers
                .Select(user => user.Key)
                .Contains(u.Id)))
            .Select(u => new UserQuizResultResponse(u.Id, u.FirstName, u.LastName, u.Username, usersWithoutTeachers[u.Id].Points))
            .OrderByDescending(u => u.Points)
            .ToList();

        await Clients.Group(groupName).SendAsync("ShowResultsCallback", response, _inMemoryQuizService.RemainingQuestions(quizId));
    }


    public async Task RemoveFromGroup(int userId, int quizId)
    {
        string groupName = quizId.ToString();
        var result = _inMemoryQuizService.RemoveUserFromGroup(quizId, userId);
        if (!result.IsSuccess)
        {
            await Clients.Caller.SendAsync("RemoveFromGroupErrorCallback", $"Unable to remove user {userId} from the group");
            return;
        }
        
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        if (result.Value is CourseEnum.Teacher)
        {
            await Clients.Group(groupName).SendAsync("RemoveFromGroupCallback", $"Teacher has left the group {groupName}");
            await Clients.Group(groupName).SendAsync("QuizQuit");
            return;
        }

        List<int> userIds = _inMemoryQuizService
           .GetAllUsersWithoutTeachers(quizId)
           .Select(user => user.Key)
           .ToList();
        List<UserResponse> response = (await _userRepository
            .GetByCondition(u => userIds.Contains(u.Id)))
            .Select(u => new UserResponse(u.Id, u.FirstName, u.LastName, u.Username))
            .ToList();
        await Clients.Group(groupName).SendAsync("RemoveFromGroupCallback", response);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        int groupId = _inMemoryQuizService.RemoveUserFromGroup(Context.ConnectionId);
        List<int> userIds = _inMemoryQuizService
           .GetAllUsersWithoutTeachers(groupId)
           .Select(user => user.Key)
           .ToList();
        List<UserResponse> response = (await _userRepository
            .GetByCondition(u => userIds.Contains(u.Id)))
            .Select(u => new UserResponse(u.Id, u.FirstName, u.LastName, u.Username))
            .ToList();
        await Clients.Group(groupId.ToString()).SendAsync("RemoveFromGroupCallback", response);
        await base.OnDisconnectedAsync(exception);
    }
}

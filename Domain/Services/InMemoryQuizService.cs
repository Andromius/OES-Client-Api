using Domain.Entities.Common;
using Domain.Entities.Courses;
using Domain.Entities.Questions;
using Domain.Entities.Questions.Options;
using Domain.Entities.Quizzes;
using Domain.Entities.Tests.Answers;
using Domain.Entities.Users;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Services;
public class InMemoryQuizService
{
    private Dictionary<int, Dictionary<int, QuizUser>> Groups { get; set; } = [];
    private Dictionary<int, Stack<Question>> QuizQuestions { get; set; } = [];
    private Dictionary<int, QuizUser> CreateGroup(int quizId)
    {
        Groups.Add(quizId, []);
        return Groups[quizId];
    }

    private void RemoveGroup(int quizId)
    {
        Groups.Remove(quizId);
    }

    public bool IsInProgress(int quizId) => Groups.Any(g => g.Key == quizId);

    public Result<CourseEnum> RemoveUserFromGroup(int quizId, int userId)
    {
        if (!Groups.TryGetValue(quizId, out Dictionary<int, QuizUser>? users))
            return Result<CourseEnum>.Failure();

        if (!users.TryGetValue(userId, out QuizUser user))
            return Result<CourseEnum>.Failure();

        CourseEnum role = user.Role;
        users.Remove(userId);
        if (users.Count <= 0)
            RemoveGroup(quizId);

        return Result<CourseEnum>.Success(role);
    }

    public void AddUserToGroup(int quizId, int userId, QuizUser quizParticipant)
    {
        if (!Groups.TryGetValue(quizId, out Dictionary<int, QuizUser>? users))
            users = CreateGroup(quizId);

        users.TryAdd(userId, quizParticipant);
    }

    public bool AddAnswers(int quizId, int userId, List<QuizAnswer> answers)
    {
        if (!Groups.TryGetValue(quizId, out Dictionary<int, QuizUser>? users))
            return false;

        if (!users.TryGetValue(userId, out QuizUser user))
            return false;

        DateTime now = DateTime.UtcNow;
        answers.ForEach(answer => answer.SubmittedAt = now);
        user.Answers.AddRange(answers);
        return true;
    }

    public int CheckAnswer(Question question, int userId)
    {
        if (!Groups.TryGetValue(question.ItemId, out Dictionary<int, QuizUser>? users))
            return 0;

        if (!users.TryGetValue(userId, out QuizUser user))
            return 0;

        int totalPoints = 0;
        List<Option> options = question.Options;
        switch (question.Type)
        {
            case QuestionType.PickOne: CheckSingle(options, user.Answers.SingleOrDefault(a => a.QuestionId == question.Id), ref totalPoints); break;
            case QuestionType.PickMany: CheckMultiple(options, user.Answers.FindAll(a => a.QuestionId == question.Id), ref totalPoints); break;
        };
        
        user.Points += totalPoints;
        return totalPoints;
    }

    public void ResetUsers(int quizId)
    {
        if (!Groups.TryGetValue(quizId, out Dictionary<int, QuizUser>? users))
            return;

        foreach (var user in users.Values)
        {
            user.Answers.Clear();
            user.Points = 0;
        }
    }

    public int GetUserPositionForQuestion(int quizId, Question question, int userId)
    {
        var onlyAttendants = GetAllUsersWithoutTeachers(quizId);
        var sorted = onlyAttendants
            .OrderByDescending(a => a.Value.Points)
            .ThenBy(a => a.Value.Answers
                .Where(ans => ans.QuestionId == question.Id)
                .FirstOrDefault()?.SubmittedAt ?? DateTime.MaxValue)
            .ToList();

        foreach (var item in sorted)
        {
            if (item.Key == userId)
                return sorted.IndexOf(item) + 1;
        }

        return int.MaxValue;
    }

    public Dictionary<int, QuizUser> GetAllUsersWithoutTeachers(int quizId)
    {
        if (!Groups.TryGetValue(quizId, out Dictionary<int, QuizUser>? users))
            return [];

        if (users.Count == 0)
            return [];

        return users
            .Where(u => u.Value.Role == CourseEnum.Attendant)
            .ToDictionary();
    }

    public Result<CourseEnum> GetGroupUserRole(int quizId, int userId)
    {
        if (!Groups.TryGetValue(quizId, out Dictionary<int, QuizUser>? users))
            return Result<CourseEnum>.Failure();

        if (!users.TryGetValue(userId, out QuizUser user))
            return Result<CourseEnum>.Failure();

        return Result<CourseEnum>.Success(user.Role);
    }

    public void AddQuizQuestions(int quizId, List<Question> questions)
    {
        ResetQuizQuestions(quizId);
        
        questions.Reverse();
        QuizQuestions.TryAdd(quizId, new(questions));
    }

    private void ResetQuizQuestions(int quizId)
    {
        QuizQuestions.Remove(quizId);
    }

    public Result<QuestionResponse> NextQuizQuestion(int quizId, int userId)
    {
        if (!QuizQuestions.TryGetValue(quizId, out Stack<Question> questions))
            return Result<QuestionResponse>.Failure();

        if (!questions.TryPeek(out Question question))
        {
            QuizQuestions.Remove(quizId);
            return Result<QuestionResponse>.Failure();
        }

        return Result<QuestionResponse>.Success(question.ToResponse(Groups[quizId][userId].Role));
    }

    public Result<Question> PopQuizQuestion(int quizId)
    {
        if (!QuizQuestions.TryGetValue(quizId, out Stack<Question> questions))
            return Result<Question>.Failure();

        if (!questions.TryPop(out Question q))
            return Result<Question>.Failure();

        return Result<Question>.Success(q);
    }

    private static void CheckSingle(List<Option> options, QuizAnswer? answer, ref int totalPoints)
    {
        if (answer is not null) totalPoints += options.Single(o => o.Id == answer.OptionId).Points;
    }

    private static void CheckMultiple(List<Option> options, List<QuizAnswer> answers, ref int totalPoints)
    {
        foreach (var option in options)
        {
            totalPoints += answers.Exists(a => a.OptionId == option.Id) ? option.Points : 0;
        }
    }

    public int RemainingQuestions(int quizId)
    {
        if (!QuizQuestions.TryGetValue(quizId, out Stack<Question> questions))
            return 0;

        return questions.Count;
    }

    public Result<QuizUser> GetTeacher(int quizId)
    {
        if (!Groups.TryGetValue(quizId, out Dictionary<int, QuizUser>? users))
            return Result<QuizUser>.Failure();

        return Result<QuizUser>.Success(users.Single(u => u.Value.Role == CourseEnum.Teacher).Value);
    }

    public int RemoveUserFromGroup(string connectionId)
    {
        var group = Groups.SingleOrDefault(g => g.Value.Any(u => u.Value.ConnectionId == connectionId));
        var user = group.Value.Where(u => u.Value.ConnectionId == connectionId).SingleOrDefault();
        RemoveUserFromGroup(group.Key, user.Key);
        return group.Key;
    }
}

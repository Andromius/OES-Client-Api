using Domain.Entities.Courses;
using Domain.Entities.Questions;
using Domain.Entities.Tests;
using Domain.Entities.Tests.Answers;
using Domain.Entities.Tests.Checkers;
using Domain.Entities.Tests.Submissions;
using Domain.Entities.Tests.Submissions.Reviews;
using Domain.Entities.Users;
using Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OESAppApi.Api.Services;
using OESAppApi.Extensions;
using OESAppApi.Models;
using Persistence;
using Soenneker.Utils.String.CosineSimilarity;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OESAppApi.Api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[Produces(Application.Json)]
[ApiController]
public class TestsController : ControllerBase
{
    private readonly ILogger<TestsController> _logger;
    private readonly OESAppApiDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly ICourseRepository _courseRepository;
    private readonly BackgroundWorkerQueue _backgroundWorkerQueue;

    public TestsController(ILogger<TestsController> logger, OESAppApiDbContext context, ITokenService tokenService,
                           ICourseRepository courseRepository, BackgroundWorkerQueue backgroundWorkerQueue)
    {
        _logger = logger;
        _context = context;
        _tokenService = tokenService;
        _courseRepository = courseRepository;
        _backgroundWorkerQueue = backgroundWorkerQueue;
    }

    // GET: api/<TestController>
    [HttpGet]
    public async Task<ActionResult<PagedList<Test>>> Get([FromQuery] int? page, [FromQuery] int? pageSize)
    {
        page ??= 1;
        pageSize ??= 5;

        int count = await _context.Test.CountAsync();
        List<Test> tests = await _context.Test.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).Include(t => t.Questions).ToListAsync();

        PagedList<Test> response = new(pageSize.Value, page.Value, count, tests);

        return Ok(response);
    }

    // GET api/<TestController>/id
    [HttpGet("{id}")]
    public async Task<ActionResult<TestResponse>> Get(int id, [FromQuery] string? password)
    {
        int userId = _tokenService.GetUserId(Request.ExtractToken());

        Test? test = await _context.Test
            .Where(x => x.Id == id)
            .Include(t => t.Questions.Where(q => q.ItemId == id))
            .ThenInclude(q => q.Options)
            .SingleOrDefaultAsync();

        if (test is null) 
            return StatusCode(StatusCodes.Status418ImATeapot);

        var result = await _courseRepository.GetUserCourseRoleAsync(test.CourseId, userId);

        if (!result.IsSuccess) 
            return BadRequest();

        if (result.Value is CourseEnum.Attendant)
        {
            List<TestSubmission> submissions = await _context.TestSubmission
                .Where(s => s.TestId == id && s.UserId == userId)
                .ToListAsync();

            if (test.Password.IsNullOrEmpty() ||
                test.Password != password ||
                submissions.Count >= test.MaxAttempts ||
                submissions.Any(s => s.Status is not Domain.Entities.Common.ESubmissionStatus.Graded))
                return StatusCode(StatusCodes.Status423Locked);
        }
        
        return Ok(test.ToResponse(result.Value));
    }

    // POST api/<TestController>
    [HttpPost]
    public async Task<ActionResult<TestResponse>> Post([FromQuery] int courseId, [FromBody] TestRequest request)
    {
        int userId = _tokenService.GetUserId(Request.ExtractToken());
        var result = await _courseRepository.GetUserCourseRoleAsync(courseId, userId);

        if (!result.IsSuccess || result.Value is CourseEnum.Attendant)
            return Forbid();
        
        Test testToAdd = request.ToTest(userId, courseId);
        var addedTest = _context.Add(testToAdd);
        await _context.SaveChangesAsync();
        return Created($"api/Test/{addedTest.Entity.Id}", addedTest.Entity.ToResponse(CourseEnum.Teacher));
    }

    [HttpGet("{id}/check-password")]
    public async Task<ActionResult> CheckPassword(int id, [FromQuery] string password)
    {
        Test t = await _context.Test.SingleAsync(t => t.Id == id);

        return t.Password == password ? Ok() : StatusCode(StatusCodes.Status423Locked);
    }

    // PUT api/<TestController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] TestRequest value)
    {
        var t = await _context.Test.SingleOrDefaultAsync(t => t.Id == id);
        if (t is null)
            return NotFound();

        _logger.LogInformation($"{nameof(Test)}: BEFORE Q DELETION");
        await _context.Question.Where(q => q.ItemId == id).ExecuteDeleteAsync();
        t.Questions.AddRange(value.Questions.ToQuestionList());
        t.Duration = value.Duration;
        t.End = value.End;
        t.Scheduled = value.Scheduled;
        t.Name = value.Name;
        t.Password = value.Password.IsNullOrEmpty() ? t.Password : value.Password;
        t.MaxAttempts = value.MaxAttempts;
        _logger.LogInformation($"{nameof(Test)}: BEFORE SAVE");
        await _context.SaveChangesAsync();
        _logger.LogInformation($"{nameof(Test)}: AFTER SAVE");

        return NoContent();
    }

    [HttpPost("submit")]
    public async Task<ActionResult<TestSubmissionResponse>> Submit([FromBody] TestSubmissionRequest submissionRequest, [FromServices] IServiceProvider serviceProvider)
    {
        int userId = _tokenService.GetUserId(Request.ExtractToken());
        var newSubmission = _context.TestSubmission.Add(submissionRequest.ToSubmission(userId));
        await _context.SaveChangesAsync();

        Test test = await _context.Test
            .Include(t => t.Questions)
            .ThenInclude(q => q.Options)
            .SingleAsync(t => t.Id == newSubmission.Entity.TestId);
        TestChecker.Check(test, newSubmission.Entity);

        var updatedSubmission = _context.TestSubmission.Update(newSubmission.Entity);
        await _context.SaveChangesAsync();
        _backgroundWorkerQueue.QueueBackgroundWorkItem(async () =>
        {
            using var scope = serviceProvider.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<OESAppApiDbContext>();
            _logger.LogInformation("RUNNING");
            int submissionsCount = await context.TestSubmission
                .Where(t => t.Id == test.Id).CountAsync();
            if (submissionsCount <= 1)
                return;
            
            List<int> openQuestionIds = test.Questions
                .Where(q => q.Type == QuestionType.Open)
                .Select(q => q.Id.Value)
                .ToList();
            if (openQuestionIds.Count == 0)
                return;
            
            await context.AnswerSimilarity.Where(a => openQuestionIds.Contains(a.QuestionId)).ExecuteDeleteAsync();
            IAsyncEnumerable<TestSubmission> submissions = context.TestSubmission
                .Where(t => t.Id == test.Id)
                .Include(t => t.Answers.Where(a => openQuestionIds.Contains(a.QuestionId)))
                .AsAsyncEnumerable();

            await foreach (var submission in submissions)
            {
                List<Answer> orderedAnswers = submission.Answers.OrderByDescending(a => a.QuestionId).ToList();
                await foreach (var submissionAgainst in context.TestSubmission
                    .Where(t => t.Id == test.Id && submission.Id != t.Id && submission.UserId != t.UserId)
                    .Include(t => t.Answers.Where(a => openQuestionIds.Contains(a.QuestionId)))
                    .AsAsyncEnumerable())
                {
                    List<Answer> orderedAgainstAnswers = submissionAgainst.Answers.OrderByDescending(a => a.QuestionId).ToList();
                    for (int i = 0; i < orderedAnswers.Count; i++)
                    {
                        double similarity = CosineSimilarityStringUtil.CalculateSimilarityPercentage(orderedAnswers[i].Text, orderedAgainstAnswers[i].Text);
                        AnswerSimilarity answerSimilarity = new()
                        {
                            Similarity = similarity,
                            SubmissionId = submission.Id,
                            CheckAgainstSubmissionId = submissionAgainst.Id,
                            QuestionId = orderedAnswers[i].QuestionId,
                        };
                        context.Add(answerSimilarity);
                    }
                }
            }
            await context.SaveChangesAsync();
            _logger.LogInformation("FINISHED");
        });
        _logger.LogInformation("RESPONDED");
        return Created($"api/tests/{test.Id}/submissions/{updatedSubmission.Entity.Id}", updatedSubmission.Entity.ToResponse());
    }

    [HttpGet("{id}/info")]
    public async Task<ActionResult<TestInfo>> Info(int id)
    {
        Test? t = await _context.Test.SingleOrDefaultAsync(t => t.Id == id);
        if (t is null) return NotFound();

        List<TestSubmission> submissions = await _context.TestSubmission
            .Where(s => s.TestId == t.Id && s.UserId == _tokenService.GetUserId(Request.ExtractToken()))
            .Include(s => s.Reviews)
            .ToListAsync();

        return Ok(t.ToInfo(submissions));
    }
    
    [Authorize(Policy = "TeacherOrAdminPolicy")]
    [HttpGet("{id}/submissions/{submissionId}")]
    public async Task<ActionResult<List<AnswerResponse>>> GetSubmissionAnswers(int id, int submissionId)
    {
        Test? test = await _context.Test
            .Include(t => t.Questions)
            .Where(t => t.Id == id)
            .SingleOrDefaultAsync();
        if (test is null)
            return NotFound();

        var result = await _courseRepository.GetUserCourseRoleAsync(test.CourseId, _tokenService.GetUserId(Request.ExtractToken()));
        if (!result.IsSuccess || result.Value is CourseEnum.Attendant)
            return Forbid();

        List<int> openQuestionIds = test.Questions
            .Where(q => q.Type == QuestionType.Open)
            .Select(q => q.Id.Value)
            .ToList();

        List<Answer> answers = await _context.Answer
            .Where(a => a.TestSubmissionId == submissionId)
            .ToListAsync();

        List<AnswerSimilarity> similarities = await _context.AnswerSimilarity
            .Where(a => a.SubmissionId == submissionId && openQuestionIds.Contains(a.QuestionId))
            .ToListAsync();

        List<AnswerResponse> response = [];
        foreach (var answer in answers)
        {
            response.Add(new(answer.Id, answer.Text, answer.QuestionId,
                similarities
                    .Where(s => s.QuestionId == answer.QuestionId)
                    .Select(s => new double?(s.Similarity)).SingleOrDefault()));
        }

        return Ok(response);
    }

    // DELETE api/<TestController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Test? t = await _context.Test.SingleOrDefaultAsync(t => t.Id == id);

        if (t is null) return NotFound();

        _context.Test.Remove(t);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [Authorize(Policy = "TeacherOrAdminPolicy")]
    [HttpPut("{id}/submissions/{submissionId}/reviews")]
    public async Task<ActionResult> PostReview(int id, int submissionId, [FromBody] List<TestSubmissionReviewRequest> request)
    {
        Test? test = await _context.Test
            .Include(t => t.Questions)
            .Where(t => t.Id == id)
            .SingleOrDefaultAsync();
        if (test is null)
            return NotFound();

        var result = await _courseRepository.GetUserCourseRoleAsync(test.CourseId, _tokenService.GetUserId(Request.ExtractToken()));
        if (!result.IsSuccess || result.Value is CourseEnum.Attendant)
            return Forbid();

        TestSubmission? submission = await _context.TestSubmission
            .Where(s => s.Id == submissionId)
            .SingleOrDefaultAsync();

        if (submission is null)
            return NotFound();

        foreach (var question in test.Questions)
        {
            var review = request.Single(r => r.QuestionId == question.Id);
            if (review.Points > question.Points)
                return BadRequest();
        }
        
        await _context.TestSubmissionReview
            .Where(r => r.SubmissionId == submissionId)
            .ExecuteDeleteAsync();

        submission.Reviews.AddRange(request.Select(r => new TestSubmissionReview() 
        {
            Points = r.Points,
            QuestionId = r.QuestionId
        }));
        
        if (submission.Status is not Domain.Entities.Common.ESubmissionStatus.Graded)
        {
            submission.Status = Domain.Entities.Common.ESubmissionStatus.Graded;
            submission.GradedAt = DateTime.UtcNow;
        }
        _context.TestSubmission.Update(submission);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [Authorize(Policy = "TeacherOrAdminPolicy")]
    [HttpGet("{id}/submissions/{submissionId}/reviews")]
    public async Task<ActionResult<List<TestSubmissionReviewResponse>>> GetReview(int id, int submissionId)
    {
        Test? test = await _context.Test
            .Where(t => t.Id == id)
            .SingleOrDefaultAsync();
        if (test is null)
            return NotFound();

        var result = await _courseRepository.GetUserCourseRoleAsync(test.CourseId, _tokenService.GetUserId(Request.ExtractToken()));
        if (!result.IsSuccess || result.Value is CourseEnum.Attendant)
            return Forbid();

        TestSubmission? submission = await _context.TestSubmission
            .Where(s => s.Id == submissionId)
            .SingleOrDefaultAsync();
        if (submission is null)
            return NotFound();

        List<TestSubmissionReviewResponse> response = await _context.TestSubmissionReview
            .Where(r => r.SubmissionId == submissionId)
            .Select(r => new TestSubmissionReviewResponse(r.QuestionId, r.Points))
            .ToListAsync();

        return Ok(response);
    }
}

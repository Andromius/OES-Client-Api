using Domain.Entities.Common;
using Domain.Entities.Courses;
using Domain.Entities.Homeworks;
using Domain.Entities.Tests;
using Domain.Entities.Tests.Submissions;
using Domain.Entities.Users;
using Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OESAppApi.Extensions;
using OESAppApi.Models;
using Persistence;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OESAppApi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "StudentOrTeacherOrAdminPolicy")]
public class UsersController : ControllerBase
{
    private readonly OESAppApiDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly ICourseRepository _courseRepository;
    private readonly IUserRepository _userRepository;

    public UsersController(OESAppApiDbContext context, ITokenService tokenService, ICourseRepository courseRepository, IUserRepository userRepository)
    {
        _context = context;
        _tokenService = tokenService;
        _courseRepository = courseRepository;
        _userRepository = userRepository;
    }

    // GET: api/<UserController>
    [HttpGet]
    public async Task<ActionResult<PagedList<UserWithRoleResponse>>> Get([FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] List<UserRole> userRoles)
    {
        page ??= 1;
        pageSize ??= 10;

        if (userRoles.Count == 0) userRoles = [UserRole.Admin, UserRole.Teacher, UserRole.Student];

        int count = await _userRepository.GetCountByCondition(u => userRoles.Contains(u.Role));
        List<UserWithRoleResponse> users = (await _userRepository.GetByCondition(page.Value, pageSize.Value, u => userRoles.Contains(u.Role)))
            .Select(u => u.ToResponse())
            .ToList();

        PagedList<UserWithRoleResponse> response = new(pageSize.Value, page.Value, count, users);

        return Ok(response);
    }

    [HttpGet("search")]
    public async Task<ActionResult<PagedList<UserWithRoleResponse>>> Search([FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] List<UserRole> userRoles, [FromQuery][MinLength(3)] string search)
    {
        page ??= 1;
        pageSize ??= 10;

        if (userRoles.Count == 0)
        {
            userRoles = [UserRole.Admin, UserRole.Teacher, UserRole.Student];
        }
        search = search.ToLower();

        int count = await _userRepository.GetCountByCondition(u => userRoles.Contains(u.Role) && 
            (u.FirstName.ToLower().Contains(search) ||
             u.LastName.ToLower().Contains(search) ||
             u.Username.ToLower().Contains(search)));
        
        List<UserWithRoleResponse> users = (await _userRepository.GetByCondition(page.Value, pageSize.Value,
            u => userRoles.Contains(u.Role) &&
            (u.FirstName.ToLower().Contains(search) ||
             u.LastName.ToLower().Contains(search) ||
             u.Username.ToLower().Contains(search)))
            )
            .Select(u => u.ToResponse())
            .ToList();

        return Ok(new PagedList<UserWithRoleResponse>(pageSize.Value, page.Value, count, users));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserWithRoleResponse>> Get(int id)
    {
        Result<User> result = await _userRepository.Get(id);
        if (!result.IsSuccess)
            return NotFound();
        
        return Ok(result.Value!.ToResponse());
    }

    [HttpGet("{id}/homework-submissions")]
    public async Task<ActionResult<List<HomeworkSubmissionResponse>>> GetHwSubmissions(int id, [FromQuery] int homeworkId)
    {
        int userId = _tokenService.GetUserId(Request.ExtractToken());
        int? courseId = await _context.Homework
            .Where(h => h.Id == homeworkId)
            .Select(h => new int?(h.CourseId))
            .SingleOrDefaultAsync();
        
        if (courseId is null)
            return Ok(Enumerable.Empty<HomeworkSubmissionResponse>());

        Result<CourseEnum> result = await _courseRepository.GetUserCourseRoleAsync(courseId.Value, userId);

        //if (courseRole is null)
        //    return Ok(Enumerable.Empty<HomeworkSubmissionResponse>());
        if (!result.IsSuccess || result.Value is CourseEnum.Attendant && id != userId)
            return Ok(Enumerable.Empty<HomeworkSubmissionResponse>());

        List<HomeworkSubmissionResponse> response = await _context.HomeworkSubmission
            .Include(x => x.Attachments)
            .Where(x => x.HomeworkId == homeworkId && x.UserId == id)
            .Select(x => new HomeworkSubmissionResponse(
                x.Id,
                x.Text,
                x.Comment,
                x.Attachments.Select(a => new HomeworkSubmissionAttachmentResponse(a.Id, a.FileName))
                .ToList()))
            .ToListAsync();
        return Ok(response);
    }

    [HttpPut("{id}/homework-scores/{homeworkId}")]
    public async Task<IActionResult> SetPoints(int id, int homeworkId, [FromBody] int points)
    {
        HomeworkScore? score = await _context.HomeworkScore
            .SingleOrDefaultAsync(x => x.UserId == id && x.HomeworkId == homeworkId);

        IActionResult responseResult;
        if (score is null)
        {
            HomeworkScore newScore = new()
            {
                HomeworkId = homeworkId,
                UserId = id,
                Points = points
            };
            _context.HomeworkScore.Add(newScore);
            responseResult = Created($"api/users/{id}/homework-scores/{homeworkId}", newScore.Points);
        }
        else
        {
            score.Points = points;
            _context.HomeworkScore.Update(score);
            responseResult = NoContent();
        }

        await _context.SaveChangesAsync();
        return responseResult;
    }

    [HttpGet("{id}/homework-scores/{homeworkId}")]
    public async Task<IActionResult> GetPoints(int id, int homeworkId)
    {
        HomeworkScore? score = await _context.HomeworkScore
            .SingleOrDefaultAsync(x => x.UserId == id && x.HomeworkId == homeworkId);

        if (score is null)
            return NotFound();

        return Ok(score.Points);
    }

    [Authorize(Policy = "TeacherOrAdminPolicy")]
    [HttpGet("{id}/test-submissions")]
    public async Task<ActionResult<List<TestSubmissionResponse>>> GetUserSubmissions(int id, [FromQuery] int testId)
    {
        Test? test = await _context.Test
            .Where(t => t.Id == testId)
            .SingleOrDefaultAsync();
        if (test is null)
            return NotFound();

        var result = await _courseRepository.GetUserCourseRoleAsync(test.CourseId, _tokenService.GetUserId(Request.ExtractToken()));
        if (!result.IsSuccess || result.Value is CourseEnum.Attendant)
            return Forbid();

        List<TestSubmissionResponse> response = await _context.TestSubmission
            .Where(s => s.TestId == testId && s.UserId == id)
            .Include(s => s.Reviews)
            .Select(s => new TestSubmissionResponse()
            {
                Id = s.Id,
                TestId = testId,
                GradedAt = s.GradedAt,
                SubmittedAt = s.SubmittedAt,
                Status = s.Status.ToString(),
                TotalPoints = s.Reviews.Count > 0 ? s.Reviews.Sum(r => r.Points) : s.TotalPoints,
            })
            .ToListAsync();
        return Ok(response);
    }
}

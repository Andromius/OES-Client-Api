using Domain.Entities.Common;
using Domain.Entities.Courses;
using Domain.Entities.Notes;
using Domain.Entities.Sessions;
using Domain.Entities.Tests;
using Domain.Entities.Users;
using Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OESAppApi.Extensions;
using OESAppApi.Models;
using Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OESAppApi.Api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly OESAppApiDbContext _context;
    private readonly CourseCodeGenerationService _courseCodeGenerationService;
    private readonly ITokenService _tokenService;

    public CoursesController(OESAppApiDbContext context, CourseCodeGenerationService courseCodeGenerationService, ITokenService tokenService)
    {
        _context = context;
        _courseCodeGenerationService = courseCodeGenerationService;
        _tokenService = tokenService;
    }

    // GET: api/<CourseController>
    [HttpGet]
    public async Task<ActionResult<PagedList<CourseResponse>>> Get([FromQuery] int? userId, [FromQuery] int? page, [FromQuery] int? pageSize)
    {
        page ??= 1;
        pageSize ??= 10;

        int count = await _context.Course.CountAsync();

        List<CourseResponse> courses;
        if (userId is not null)
        {
            List<CourseXUser> courseXUsers = await _context.CourseXUser.Where(cxu => cxu.UserId == userId).ToListAsync();
            List<int> courseIds = courseXUsers.Select(cxu => cxu.CourseId).ToList();
            courses = await _context.Course
                .Where(c => courseIds.Contains(c.Id)).Skip((page.Value - 1) * pageSize.Value)
                .Take(pageSize.Value)
                .OrderByDescending(c => c.ShortName)
                .Select(c => c.ToResponse())
                .ToListAsync();
        }
        else
        {
            courses = await _context.Course
                .Skip((page.Value - 1) * pageSize.Value)
                .Take(pageSize.Value)
                .OrderByDescending(c => c.ShortName)
                .Select(c => c.ToResponse())
                .ToListAsync();
        }

        PagedList<CourseResponse> response = new(pageSize.Value, page.Value, count, courses);

        return Ok(response);
    }

    // GET api/<CourseController>/5
    [HttpGet("{id}/items")]
    public async Task<IEnumerable<CourseItemResponse>> Get(int id)
    {
        int userId = _tokenService.GetUserId(Request.ExtractToken());
        List<CourseItemResponse> response = await _context.CourseItem
            .Where(item => item.CourseId == id && item.CourseItemType != ECourseItemType.UserQuiz)
            .Select(item => item.ToItemResponse())
            .ToListAsync();

        List<int> userQuizIds = await _context.UserQuizUserPermission
            .Where(p => p.UserId == userId)
            .Select(p => p.UserQuizId)
            .ToListAsync();

        List<CourseItemResponse> userQuizResponse = await _context.CourseItem
            .Where(item => item.CourseId == id && item.CourseItemType == ECourseItemType.UserQuiz &&
                (item.UserId == userId || userQuizIds.Contains(item.Id)))
            .Select(item => item.ToItemResponse())
            .ToListAsync();

        response.AddRange(userQuizResponse);
        return response;
    }

    [HttpGet("{id}")]
    public async Task<CourseResponse> GetId(int id)
    {
        Course c = await _context.Course.SingleAsync(c => c.Id == id);
        return c.ToResponse();
    }

	// POST api/<CourseController>
	[Authorize(Policy = "TeacherOrAdminPolicy")]
	[HttpPost]
    public async Task<ActionResult<CourseResponse>> Post([FromBody] CourseRequest request)
    {
        Course c = request.ToCourse(_courseCodeGenerationService);
        request.TeacherIds.ForEach(tId => c.Users.Add(new(tId, CourseEnum.Teacher)));
        request.AttendantIds.ForEach(sId => c.Users.Add(new(sId, CourseEnum.Attendant)));

        var newCourse = _context.Course.Add(c);
        await _context.SaveChangesAsync();

        return Created($"api/courses/{newCourse.Entity.Id}", newCourse.Entity.ToResponse());
    }

    // PUT api/<CourseController>/5
    [Authorize(Policy = "TeacherOrAdminPolicy")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] CourseRequest request)
    {
        Course courseToUpdate = await _context.Course
            .Include(c => c.Users)
            .SingleAsync(c => c.Id == id);
        courseToUpdate.UpdateFromRequest(request);
        _context.Course.Update(courseToUpdate);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }

	// DELETE api/<CourseController>/5
	[Authorize(Policy = "TeacherOrAdminPolicy")]
	[HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _context.Course.Where(c => c.Id == id).ExecuteDeleteAsync();
        
        return NoContent();
    }

    [Authorize(Policy = "TeacherOrAdminPolicy")]
    [HttpPut("{id}/code")]
    public async Task<string> GenerateCourseCode(int id)
    {
        string code = _courseCodeGenerationService.GenerateUniqueCode();
        Course course = await _context.Course.Where(c => c.Id == id).SingleAsync();
        course.Code = code;
        _context.Update(course);
        await _context.SaveChangesAsync();
        return code;
    }

    [Authorize(Policy = "TeacherOrAdminPolicy")]
    [HttpGet("{id}/code")]
    public async Task<string> GetCourseCode(int id)
    {
        return await _context.Course.Where(c => c.Id == id).Select(c => c.Code).SingleAsync();
    }

	[HttpPut("{code}/join")]
    public async Task<IActionResult> JoinCourse(string code)
    {
        int userId = _tokenService.GetUserId(Request.ExtractToken());

        Course? course = await _context.Course
            .Where(c => c.Code == code)
            .Include(c => c.Users.Where(u => u.UserRole == CourseEnum.Attendant))
            .SingleOrDefaultAsync();

        if (course == null) return NotFound();
        if (course.Users.Exists(u => u.UserId == userId)) return NoContent();

        course.Users.Add(new CourseXUser(userId, CourseEnum.Attendant));
        
        _context.Course.Update(course);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("{id}/users")]
    public async Task<IActionResult> GetUsers(int id, [FromQuery] List<CourseEnum> userCourseRoles, [FromQuery] bool withRole = true)
    {
        if (userCourseRoles.Count == 0)
            userCourseRoles = [CourseEnum.Teacher, CourseEnum.Attendant];

        List<int> userIds = await _context.CourseXUser
            .Where(cxu => cxu.CourseId == id && userCourseRoles.Contains(cxu.UserRole))
            .Select(cxu => cxu.UserId)
            .ToListAsync();

        if (withRole)
        {
            List<UserWithRoleResponse> withRoleResponse = await _context.User
                .Where(u => userIds.Contains(u.Id))
                .Select(u => new UserWithRoleResponse(u.Id, u.FirstName, u.LastName, u.Username, u.Role))
                .ToListAsync();
            return Ok(withRoleResponse);
        }

        List<UserResponse> response = await _context.User
            .Where(u => userIds.Contains(u.Id))
            .Select(u => new UserResponse(u.Id, u.FirstName, u.LastName, u.Username))
            .ToListAsync();
        return Ok(response);
    }

}

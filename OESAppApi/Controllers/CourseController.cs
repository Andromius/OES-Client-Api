using Domain.Entities.Courses;
using Domain.Entities.Notes;
using Domain.Entities.Sessions;
using Domain.Entities.Tests;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OESAppApi.Models;
using Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OESAppApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly OESAppApiDbContext _context;
    private readonly CourseCodeGenerationService _courseCodeGenerationService;

    public CourseController(OESAppApiDbContext context, CourseCodeGenerationService courseCodeGenerationService)
    {
        _context = context;
        _courseCodeGenerationService = courseCodeGenerationService;
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

        PagedList<CourseResponse> response = new (pageSize.Value, page.Value, count, courses);

        return Ok(response);
    }

    // GET api/<CourseController>/5
    [HttpGet("items/{id}")]
    public async Task<IEnumerable<CourseItemResponse>> Get(int id)
    {
        List<Test> tests = await _context.Test.Where(t => t.CourseId == id).ToListAsync();
        List<CourseItemResponse> notes = await _context.Note
            .Where(n => n.CourseId == id)
            .Select(n => n.ToItemResponse(nameof(Note), n.IsVisible))
            .ToListAsync();

        List<CourseItemResponse> response = new();
        foreach (var test in tests)
        {
            response.Add(test.ToItemResponse(nameof(Test), test.IsVisible));
        }

        response.AddRange(notes);
        return response;
    }

    [HttpGet("{id}")]
    public async Task<CourseResponse> GetId(int id)
    {
        Course c = await _context.Course.SingleAsync(c => c.Id == id);
        return c.ToResponse();
    }

    // POST api/<CourseController>
    [HttpPost]
    public async Task<ActionResult<CourseResponse>> Post([FromBody] CourseRequest request)
    {
        Course c = request.ToCourse(_courseCodeGenerationService);
        request.TeacherIds.ForEach(tId => c.Users.Add(new (tId, CourseEnum.Teacher)));
        request.AttendantIds.ForEach(sId => c.Users.Add(new (sId, CourseEnum.Attendant)));
        
        var newCourse = _context.Course.Add(c);
        await _context.SaveChangesAsync();

        return Created($"api/Course/{newCourse.Entity.Id}", newCourse.Entity.ToResponse());   
    }

    // PUT api/<CourseController>/5
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
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _context.Course.Where(c => c.Id == id).ExecuteDeleteAsync();
        
        return NoContent();
    }

    [Authorize(Policy = "TeacherOrAdminPolicy")]
    [HttpPut("code/{courseId}")]
    public async Task<string> GenerateCourseCode(int courseId)
    {
        string code = _courseCodeGenerationService.GenerateUniqueCode();
        Course course = await _context.Course.Where(c => c.Id == courseId).SingleAsync();
        course.Code = code;
        _context.Update(course);
        await _context.SaveChangesAsync();
        return code;
    }

    [Authorize(Policy = "TeacherOrAdminPolicy")]
    [HttpGet("code/{courseId}")]
    public async Task<string> GetCourseCode(int courseId)
    {
        return await _context.Course.Where(c => c.Id == courseId).Select(c => c.Code).SingleAsync();
    }

    [HttpPut("join/{code}")]
    public async Task<IActionResult> JoinCourse(string code)
    {
        string tokenString = Request.Headers["Authorization"].Single()!.Replace("Bearer ", "");
        var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
        int userId = Convert.ToInt32(token.Payload["userId"]);
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

}

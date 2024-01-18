using Domain.Entities.Courses;
using Domain.Entities.Tests;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OESAppApi.Models;
using Persistence;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OESAppApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "StudentOrTeacherOrAdminPolicy")]
public class UserController : ControllerBase
{
    private readonly OESAppApiDbContext _context;

    public UserController(OESAppApiDbContext context)
    {
        _context = context;
    }

    // GET: api/<UserController>
    [HttpGet]
    public async Task<ActionResult<PagedList<UserResponse>>> Get([FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] List<UserRole> userRoles)
    {
        page ??= 1;
        pageSize ??= 10;

        if (userRoles.Count == 0)
        {
            userRoles = new() { UserRole.Admin, UserRole.Teacher, UserRole.Student };
        }

        int count = await _context.User.Where(u => userRoles.Contains(u.Role)).CountAsync();
        List<UserResponse> users = await _context.User
            .Where(u => userRoles.Contains(u.Role))
            .Skip((page.Value - 1) * pageSize.Value)
            .Take(pageSize.Value)
            .Select(u => u.ToResponse())
            .ToListAsync();
       
        PagedList<UserResponse> response = new(pageSize.Value, page.Value, count, users);

        return Ok(response);
    }
    //fname lname username
    [HttpGet("search")]
    public async Task<ActionResult<PagedList<UserResponse>>> Search([FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] List<UserRole> userRoles, [FromQuery] [MinLength(3)] string search)
    {
        page ??= 1;
        pageSize ??= 10;

        if (userRoles.Count == 0)
        {
            userRoles = new() { UserRole.Admin, UserRole.Teacher, UserRole.Student };
        }
        search = search.ToLower();

        int count = await _context.User.Where(u => userRoles.Contains(u.Role) && (u.FirstName.ToLower().Contains(search) || u.LastName.ToLower().Contains(search) || u.Username.ToLower().Contains(search))).CountAsync();
        List<UserResponse> users = await _context.User
            .Where(u => userRoles.Contains(u.Role) && (u.FirstName.ToLower().Contains(search) || u.LastName.ToLower().Contains(search) || u.Username.ToLower().Contains(search)))
            .Skip((page.Value - 1) * pageSize.Value)
            .Take(pageSize.Value)
            .Select(u => u.ToResponse())
            .ToListAsync();

        return Ok(new PagedList<UserResponse>(pageSize.Value, page.Value, count, users));
    }

    [HttpGet("courseUsers")]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetCourseUsers([FromQuery] int courseId, [FromQuery] List<CourseEnum> userCourseRoles)
    {
        if (userCourseRoles.Count == 0)
            userCourseRoles = new() { CourseEnum.Teacher, CourseEnum.Attendant };

        List<int> userIds = await _context.CourseXUser
            .Where(cxu => cxu.CourseId == courseId && userCourseRoles.Contains(cxu.UserRole))
            .Select(cxu => cxu.UserId)
            .ToListAsync();

        return await _context.User
            .Where(u => userIds.Contains(u.Id))
            .Select(u => u.ToResponse())
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> Get(int id)
    {
        User u = await _context.User.SingleAsync(x => x.Id == id);
        return u.ToResponse();
    }
    // POST api/<UserController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<UserController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<UserController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}

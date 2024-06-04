using Domain.Entities.Courses;
using Domain.Entities.Notes;
using Domain.Entities.Tests;
using Domain.Entities.Users;
using Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OESAppApi.Extensions;
using Persistence;

namespace OESAppApi.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class NotesController : ControllerBase
{
    private readonly OESAppApiDbContext _context;
    private readonly ICourseRepository _courseRepository;
    private readonly ITokenService _tokenService;

	public NotesController(OESAppApiDbContext context, ICourseRepository courseRepository, ITokenService tokenService)
	{
		_context = context;
		_courseRepository = courseRepository;
		_tokenService = tokenService;
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "StudentOrTeacherOrAdminPolicy")]
    [HttpGet("{id}")]
    public async Task<ActionResult<NoteResponse>> Get(int id)
    {
        int userId = _tokenService.GetUserId(Request.ExtractToken());
        Note? note = await _context.Note
            .Where(n => n.Id == id)
            .SingleOrDefaultAsync();
        if (note is null)
            return NotFound();
        var result = await _courseRepository.GetUserCourseRoleAsync(note.CourseId, userId);
        if (!result.IsSuccess)
            return Forbid(JwtBearerDefaults.AuthenticationScheme);
		return Ok(note.ToResponse());
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "TeacherOrAdminPolicy")]
    [HttpPost]
    public async Task<ActionResult<NoteResponse>> Post([FromQuery] int courseId, [FromBody] NoteRequest request)
    {
        Course course = await _context.Course.SingleAsync(c => c.Id == courseId);
        User user = await _context.User.SingleAsync(u => u.Id == request.CreatedById);

        Note noteToAdd = request.ToNote(user, course);
        var addedNote = _context.Note.Add(noteToAdd);
        await _context.SaveChangesAsync();
        return Created($"api/notes/{addedNote.Entity.Id}", addedNote.Entity.ToResponse());
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "TeacherOrAdminPolicy")]
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] NoteRequest request)
    {
        Note note = await _context.Note.SingleAsync(n => n.Id == id);
        note.Data = request.Data;
        note.IsVisible = request.IsVisible;
        note.Name = request.Name;
        _context.Note.Update(note);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "TeacherOrAdminPolicy")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Note? note = await _context.Note.SingleOrDefaultAsync(n => n.Id == id);

        if (note is null) return NotFound();

        _context.Note.Remove(note);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

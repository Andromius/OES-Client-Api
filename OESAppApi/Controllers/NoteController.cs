using Domain.Entities.Courses;
using Domain.Entities.Notes;
using Domain.Entities.Tests;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace OESAppApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class NoteController : ControllerBase
{
    private readonly OESAppApiDbContext _context;

    public NoteController(OESAppApiDbContext context)
    {
        _context = context;
    }

    [Authorize(Policy = "StudentOrTeacherOrAdminPolicy")]
    [HttpGet("{id}")]
    public async Task<ActionResult<NoteResponse>> Get(int id, [FromQuery] int courseId)
    {
        NoteResponse? response = await _context.Note.Where(n => n.Id == id && n.CourseId == n.CourseId).Select(n => n.ToResponse()).SingleOrDefaultAsync();
        return response is not null ? Ok(response) : NotFound();
    }
    
    [Authorize(Policy = "TeacherOrAdminPolicy")]
    [HttpPost]
    public async Task<ActionResult<NoteResponse>> Post([FromQuery] int courseId, [FromBody] NoteRequest request)
    {
        Course course = await _context.Course.SingleAsync(c => c.Id == courseId);
        User user = await _context.User.SingleAsync(u => u.Id == request.CreatedById);

        Note noteToAdd = request.ToNote(user, course);
        var addedNote = _context.Note.Add(noteToAdd);
        await _context.SaveChangesAsync();
        return Created($"api/Note/{addedNote.Entity.Id}?courseId={courseId}", addedNote.Entity.ToResponse());
    }

    [Authorize(Policy = "TeacherOrAdminPolicy")]
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromQuery] int courseId, [FromBody] NoteRequest request)
    {
        Note note = await _context.Note.SingleAsync(n => n.Id == id && n.CourseId == courseId);
        note.Data = request.Data;
        note.IsVisible = request.IsVisible;
        note.Name = request.Name;
        _context.Note.Update(note);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [Authorize(Policy = "TeacherOrAdminPolicy")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id, [FromQuery] int courseId)
    {
        Note? note = await _context.Note.SingleOrDefaultAsync(n => n.Id == id && n.CourseId == courseId);

        if (note is null) return NotFound();

        _context.Note.Remove(note);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

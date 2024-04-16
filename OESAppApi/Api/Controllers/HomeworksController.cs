using Domain.Entities.Courses;
using Domain.Entities.Homeworks;
using Domain.Entities.Users;
using Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NuGet.Protocol;
using OESAppApi.Extensions;
using Persistence;
using Persistence.Repositories;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Security.Permissions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OESAppApi.Api.Controllers;
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class HomeworksController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly OESAppApiDbContext _context;
    private readonly ICourseRepository _courseRepository;
    public HomeworksController(ITokenService tokenService, OESAppApiDbContext context, ICourseRepository courseRepository)
    {
        _tokenService = tokenService;
        _context = context;
        _courseRepository = courseRepository;
    }

    [HttpPost("{id}/submissions")]
    [DisableRequestSizeLimit]
    public async Task<ActionResult> Submit(int id, [FromForm] HomeworkSubmissionRequest request, [FromServices] IHomeworkSubmissionAttachmentRepository repository)
    {
        HomeworkSubmission newSubmission = new(
            _tokenService.GetUserId(Request.ExtractToken()),
            id,
            request.Text);

        var submissionEntity = _context.Add(newSubmission);
        await _context.SaveChangesAsync();

        if (request.FormFiles is not null)
        {
            foreach (var file in request.FormFiles)
            {
                await repository.SaveAttachmentAsync(file, submissionEntity.Entity.Id);
            }
        }
        //if (Request.HasFormContentType)
        //{
        //    var contentTypeHeader = MediaTypeHeaderValue.Parse(Request.ContentType!);
        //    contentTypeHeader.Parameters
        //}
        //MultipartReader reader = new(".GetBoundary()", Request.Body);
        //MultipartSection? section = await reader.ReadNextSectionAsync();
        //section.Body
        return NoContent();
    }

    [HttpGet("attachments/{id}")]
    public async Task<IActionResult> GetAttachment(int id, [FromServices] IHomeworkSubmissionAttachmentRepository repository)
    {
        var data = await repository.GetAttachmentDataAsync(id);
            
        return data is not null ? File(data, "application/octet-stream") : NotFound();
    }

    [HttpPatch("{id}/submissions/{submissionId}")]
    [Authorize(Policy = "TeacherOrAdminPolicy")]
    public async Task<IActionResult> ChangeCommentOnSubmission(int id, int submissionId, [FromBody] string comment)
    {
        int userId = _tokenService.GetUserId(Request.ExtractToken());
        int? courseId = await _context.Homework
            .Where(h => h.Id == id)
            .Select(h => new int?(h.CourseId))
            .SingleOrDefaultAsync();

        if (courseId is null)
            return NotFound();

        var result = await _courseRepository.GetUserCourseRoleAsync(courseId.Value, userId);

        if (!result.IsSuccess)
            return Forbid();
        
        HomeworkSubmission? submission = await _context.HomeworkSubmission
            .SingleOrDefaultAsync(s => s.HomeworkId == id && s.Id == submissionId);
        
        if (submission is null)
            return NotFound();

        submission.Comment = comment;
        _context.Update(submission);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("img")]
    [AllowAnonymous]
    public async Task<ActionResult> GetImagePrivate()
    {
        HomeworkSubmission homeworkSubmission = await _context.HomeworkSubmission.Include(h => h.Attachments).OrderByDescending(h => h.Id).LastAsync();
        return Ok(homeworkSubmission.Attachments.First().File);
    }

    [HttpPost]
    public async Task<ActionResult<HomeworkResponse>> Post([FromQuery] int courseId, [FromBody] HomeworkRequest request)
    {
        Homework h = request.ToHomework(courseId, _tokenService.GetUserId(Request.ExtractToken()));
        var newHomework = _context.Add(h);
        await _context.SaveChangesAsync();

        return Created($"api/homeworks/{newHomework.Entity.Id}", newHomework.Entity.ToResponse());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HomeworkResponse>> Get(int id)
    {
        HomeworkResponse? h = await _context.Homework
            .Where(h => h.Id == id)
            .Select(h => h.ToResponse())
            .SingleOrDefaultAsync();

        return h is not null ? Ok(h) : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] HomeworkRequest request)
    {
        Homework? h = await _context.Homework.SingleOrDefaultAsync(h => h.Id == id); 
        if (h is null)
            return NotFound();

        _context.Update(h.UpdateFromRequest(request));
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "TeacherOrAdminPolicy")]
    public async Task<ActionResult> Delete(int id)
    {
        Homework? h = await _context.Homework.SingleOrDefaultAsync(h => h.Id == id);
        if (h is null) return NotFound();

        _context.Remove(h);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

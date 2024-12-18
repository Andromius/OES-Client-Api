using Domain.Entities.Common;
using Domain.Entities.Courses;
using Domain.Entities.Questions;
using Domain.Entities.Quizzes;
using Domain.Entities.UserQuizzes;
using Domain.Entities.Users;
using Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OESAppApi.Extensions;
using Persistence;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OESAppApi.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserQuizzesController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly OESAppApiDbContext _context;
    private readonly ICourseRepository _courseRepository;

    public UserQuizzesController(ITokenService tokenService, OESAppApiDbContext context, ICourseRepository courseRepository)
    {
        _tokenService = tokenService;
        _context = context;
        _courseRepository = courseRepository;
    }

    // GET api/<UserQuizzesController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<QuizResponse>> Get(int id)
    {
        int userId = _tokenService.GetUserId(Request.ExtractToken());

        UserQuiz? userQuiz = await _context.UserQuiz
            .Where(q => q.Id == id)
            .Include(q => q.Questions.Where(q => q.ItemId == id))
            .ThenInclude(q => q.Options)
            .SingleOrDefaultAsync();

        if (userQuiz is null)
            return NotFound();

        var permission = await _context.UserQuizUserPermission.Where(p => p.UserId == userId && p.UserQuizId == id).SingleOrDefaultAsync();
        if (userQuiz.UserId != userId && permission is null)
            return Forbid(JwtBearerDefaults.AuthenticationScheme);

        var result = await _courseRepository.GetUserCourseRoleAsync(userQuiz.CourseId, userId);
        if (!result.IsSuccess)
            return StatusCode(StatusCodes.Status418ImATeapot);

        return Ok(userQuiz.ToResponse(result.Value));
    }
    // POST api/<UserQuizzesController>
    [HttpPost]
    public async Task<ActionResult<UserQuizResponse>> Post([FromQuery] int courseId, [FromBody] UserQuizRequest request)
    {
        int userId = _tokenService.GetUserId(Request.ExtractToken());
        var result = await _courseRepository.GetUserCourseRoleAsync(courseId, userId);

        if (!result.IsSuccess)
            return Forbid(JwtBearerDefaults.AuthenticationScheme);

        UserQuiz q = request.ToUserQuiz(courseId, _tokenService.GetUserId(Request.ExtractToken()));
        var newQuiz = _context.Add(q);
        await _context.SaveChangesAsync();

        return Created($"api/userquizzes/{newQuiz.Entity.Id}", newQuiz.Entity.ToResponse(result.Value));
    }

    // PUT api/<UserQuizzesController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] UserQuizRequest value)
    {
        int userId = _tokenService.GetUserId(Request.ExtractToken());
        UserQuiz? q = await _context.UserQuiz.SingleOrDefaultAsync(q => q.Id == id);
        if (q is null)
            return NotFound();

        var permission = await _context.UserQuizUserPermission
            .Where(p => p.UserId == userId && p.UserQuizId == id && p.Permission == EUserPermission.Edit)
            .SingleOrDefaultAsync();
        if (q.UserId != userId && permission is null)
            return Forbid(JwtBearerDefaults.AuthenticationScheme);

        await _context.Question.Where(q => q.ItemId == id).ExecuteDeleteAsync();
        q.Questions = value.Questions.ToQuestionList();
        q.Name = value.Name;
        q.ShouldShuffleQuestions = value.ShouldShuffleQuestions;

        _context.UserQuiz.Update(q);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE api/<UserQuizzesController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        int userId = _tokenService.GetUserId(Request.ExtractToken());
        UserQuiz? q = await _context.UserQuiz.SingleOrDefaultAsync(q => q.Id == id && q.UserId == userId);
        if (q is null) 
            return NotFound();
        
        var permission = await _context.UserQuizUserPermission
            .Where(p => p.UserId == userId && p.UserQuizId == id && p.Permission == EUserPermission.Edit)
            .SingleOrDefaultAsync();
        if (q.UserId != userId && permission is null)
            return Forbid(JwtBearerDefaults.AuthenticationScheme);

        _context.UserQuiz.Remove(q);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("{id}/permissions")]
    public async Task<ActionResult<UserQuizUserPermissionResponse>> AddPermission(int id, [FromBody] UserQuizUserPermissionRequest request)
    {
        int userId = _tokenService.GetUserId(Request.ExtractToken());
        var userQuiz = await _context.UserQuiz
            .Include(u => u.UserPermissions)
            .SingleOrDefaultAsync(u => u.Id == id);
        if (userQuiz is null)
            return NotFound();

        var editorPermission = await _context.UserQuizUserPermission
            .Where(p => p.UserQuizId == id && p.UserId == userId && p.Permission == EUserPermission.Edit)
            .SingleOrDefaultAsync();
        if (userQuiz.UserId != userId && editorPermission is null)
            return Forbid(JwtBearerDefaults.AuthenticationScheme);

        if (userQuiz.UserPermissions.Any(u => u.UserId == request.UserId))
            return BadRequest();

        userQuiz.UserPermissions.Add(new()
        {
            UserId = request.UserId,
            Permission = request.Permission
        });

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut("{id}/permissions")]
    public async Task<ActionResult<UserQuizUserPermissionResponse>> UpdatePermission(int id, [FromBody] UserQuizUserPermissionRequest request)
    {
        int userId = _tokenService.GetUserId(Request.ExtractToken());
        var userQuiz = await _context.UserQuiz
            .Include(u => u.UserPermissions)
            .SingleOrDefaultAsync(u => u.Id == id);
        if (userQuiz is null)
            return NotFound();

        var editorPermission = await _context.UserQuizUserPermission
            .Where(p => p.UserQuizId == id && p.UserId == userId && p.Permission == EUserPermission.Edit)
            .SingleOrDefaultAsync();
        if (userQuiz.UserId != userId && editorPermission is null)
            return Forbid(JwtBearerDefaults.AuthenticationScheme);

        var permission = userQuiz.UserPermissions.SingleOrDefault(u => u.UserId == request.UserId);
        if (permission is null)
            return BadRequest();

        permission.Permission = request.Permission;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("{id}/permissions/{permissionUserId}")]
    public async Task<ActionResult<UserQuizUserPermissionResponse>> RemovePermission(int id, int permissionUserId)
    {
        int userId = _tokenService.GetUserId(Request.ExtractToken());
        var userQuiz = await _context.UserQuiz
            .Include(u => u.UserPermissions)
            .SingleOrDefaultAsync(u => u.Id == id);
        if (userQuiz is null)
            return NotFound();
        
        var editorPermission = await _context.UserQuizUserPermission
            .Where(p => p.UserQuizId == id && p.UserId == userId && p.Permission == EUserPermission.Edit)
            .SingleOrDefaultAsync();
        if (userQuiz.UserId != userId && editorPermission is null && userId != permissionUserId)
            return Forbid(JwtBearerDefaults.AuthenticationScheme);

        var permission = userQuiz.UserPermissions.SingleOrDefault(u => u.UserId == permissionUserId);
        if (permission is null)
            return BadRequest();

        userQuiz.UserPermissions.Remove(permission);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("{id}/permissions")]
    public async Task<ActionResult<UserQuizUserPermissionResponse>> GetPermissions(int id)
    {
        int userId = _tokenService.GetUserId(Request.ExtractToken());
        var userQuiz = await _context.UserQuiz.SingleOrDefaultAsync(u => u.Id == id);
        if (userQuiz is null)
            return NotFound();
        
        var permission = await _context.UserQuizUserPermission.Where(p => p.UserQuizId == id && p.UserId == userId).SingleOrDefaultAsync();
        if (userQuiz.UserId != userId && permission is null)
            return Forbid(JwtBearerDefaults.AuthenticationScheme);

        List<UserQuizUserPermissionResponse> permissions = await _context.UserQuizUserPermission
            .Include(p => p.User)
            .Where(p => p.UserQuizId == id)
            .Select(p => new UserQuizUserPermissionResponse(p.UserId, p.Permission,
                p.User.FirstName, p.User.LastName, p.User.Username))
            .ToListAsync();

        return Ok(permissions);
    }
}

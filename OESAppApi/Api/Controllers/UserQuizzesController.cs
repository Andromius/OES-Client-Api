using Domain.Entities.Courses;
using Domain.Entities.Questions;
using Domain.Entities.Quizzes;
using Domain.Entities.UserQuizzes;
using Domain.Entities.Users;
using Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
            .Where(q => q.Id == id && q.UserId == userId)
            .Include(q => q.Questions.Where(q => q.ItemId == id))
            .ThenInclude(q => q.Options)
            .SingleOrDefaultAsync();

        if (userQuiz is null)
            return NotFound();

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
            return Forbid();

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
        UserQuiz? q = await _context.UserQuiz.SingleOrDefaultAsync(q => q.Id == id && q.UserId == userId);
        if (q is null)
            return NotFound();

        await _context.Question.Where(q => q.ItemId == id).ExecuteDeleteAsync();
        q.Questions = value.Questions.ToQuestionList();
        q.Name = value.Name;

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

        if (q is null) return NotFound();

        _context.UserQuiz.Remove(q);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

using Domain.Entities.Courses;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OESAppApi.Extensions;
using Domain.Entities.Quizzes;
using Persistence;
using Domain.Entities.Questions;
using Domain.Entities.Users;
using Domain.Entities.Homeworks;
using Microsoft.AspNetCore.Authentication.JwtBearer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OESAppApi.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "TeacherOrAdminPolicy")]
public class QuizzesController : ControllerBase
{
    private readonly InMemoryQuizService _inMemoryQuizService;
    private readonly ITokenService _tokenService;
    private readonly OESAppApiDbContext _context;
    private readonly ICourseRepository _courseRepository;

    public QuizzesController(InMemoryQuizService inMemoryQuizService, ITokenService tokenService, OESAppApiDbContext context, ICourseRepository courseRepository)
    {
        _inMemoryQuizService = inMemoryQuizService;
        _tokenService = tokenService;
        _context = context;
        _courseRepository = courseRepository;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<QuizResponse>> Get(int id)
    {
        int userId = _tokenService.GetUserId(Request.ExtractToken());

        Quiz? quiz = await _context.Quiz
            .Where(q => q.Id == id)
            .Include(q => q.Questions.Where(q => q.ItemId == id))
            .ThenInclude(q => q.Options)
            .SingleOrDefaultAsync();

        if (quiz is null)
            return StatusCode(StatusCodes.Status418ImATeapot);

        var result = await _courseRepository.GetUserCourseRoleAsync(quiz.CourseId, userId);

        if (!result.IsSuccess)
            return BadRequest();

        if (result.Value is CourseEnum.Attendant)
            return StatusCode(StatusCodes.Status423Locked);

        return Ok(quiz.ToResponse(result.Value));
    }

    // POST api/<QuizzesController>
    [HttpPost]
    public async Task<ActionResult<QuizResponse>> Post([FromQuery] int courseId, [FromBody] QuizRequest request)
    {
        int userId = _tokenService.GetUserId(Request.ExtractToken());
        var result = await _courseRepository.GetUserCourseRoleAsync(courseId, userId);

        if (!result.IsSuccess || result.Value is CourseEnum.Attendant)
            return Forbid(JwtBearerDefaults.AuthenticationScheme);

        Quiz q = request.ToQuiz(courseId, _tokenService.GetUserId(Request.ExtractToken()));
        var newQuiz = _context.Add(q);
        await _context.SaveChangesAsync();

        return Created($"api/quizzes/{newQuiz.Entity.Id}", newQuiz.Entity.ToResponse(result.Value));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] QuizRequest value)
    {
        if (_inMemoryQuizService.IsInProgress(id))
            return StatusCode(StatusCodes.Status423Locked);

        Quiz? q = await _context.Quiz.SingleOrDefaultAsync(t => t.Id == id);
        if (q is null)
            return NotFound();

        await _context.Question.Where(q => q.ItemId == id).ExecuteDeleteAsync();
        q.Questions = value.Questions.ToQuestionList();
        q.End = value.End;
        q.Name = value.Name;

        _context.Quiz.Update(q);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE api/<QuizzesController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Quiz? q = await _context.Quiz.SingleOrDefaultAsync(t => t.Id == id);

        if (q is null) return NotFound();

        _context.Quiz.Remove(q);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

using Domain.Entities.Courses;
using Domain.Entities.Tests;
using Domain.Entities.Tests.Answers;
using Domain.Entities.Tests.Questions;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OESAppApi.Models;
using Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OESAppApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[Produces(Application.Json)]
[ApiController]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;
    private readonly OESAppApiDbContext _context;

    public TestController(ILogger<TestController> logger, OESAppApiDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    // GET: api/<TestController>
    [HttpGet]
    public async Task<ActionResult<PagedList<Test>>> Get([FromQuery] int? page, [FromQuery] int? pageSize)
    {
        page ??= 1;
        pageSize ??= 5;

        int count = await _context.Test.CountAsync();
        List<Test> tests = await _context.Test.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).Include(t => t.Questions).ToListAsync();

        PagedList<Test> response = new(pageSize.Value, page.Value, count, tests);

        return Ok(response);
    }

    // GET api/<TestController>/id
    [HttpGet("{id}")]
    public async Task<ActionResult<TestResponse>> Get(int id, [FromQuery] int courseId)
    {
        string tokenString = Request.Headers["Authorization"].Single()!.Replace("Bearer ", "");
        var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
        int userId = Convert.ToInt32(token.Payload["userId"]);

        CourseEnum who = await _context.CourseXUser
            .Where(cxu => cxu.UserId == userId && cxu.CourseId == courseId)
            .Select(cxu => cxu.UserRole)
            .SingleOrDefaultAsync();

        Test? test = await _context.Test
            .Include(t => t.Questions.Where(q => q.TestId == id))
            .ThenInclude(q => q.Options)
            .SingleOrDefaultAsync(x => x.Id == id && x.CourseId == courseId);
        if (test is not null)
        {
            return Ok(test.ToResponse(who));
        }
        return StatusCode(StatusCodes.Status418ImATeapot);
    }

    // POST api/<TestController>
    [HttpPost]
    public async Task<ActionResult<TestResponse>> Post([FromQuery] int courseId, [FromBody] TestRequest request)
    {
        Course course = await _context.Course.SingleAsync(c => c.Id == courseId);
        User user = await _context.User.SingleAsync(u => u.Id == request.CreatedById);
        
        Test testToAdd = request.ToTest(user, course);
        var addedTest = _context.Test.Add(testToAdd);
        await _context.SaveChangesAsync();
        return Created($"api/Test/{addedTest.Entity.Id}", addedTest.Entity.ToResponse(CourseEnum.Teacher));
    }

    [HttpGet("checkpassword")]
    public async Task<ActionResult> Get([FromQuery] int id, [FromQuery] string password, [FromQuery] int courseId)
    {
        Test t = await _context.Test.SingleAsync(t => t.Id == id && t.CourseId == courseId);

        return t.Password == password ? Ok() : StatusCode(StatusCodes.Status423Locked);
    }

    // PUT api/<TestController>/5
    [HttpPut]
    public async Task<ActionResult> Put([FromQuery] int id, [FromBody] TestRequest value)
    {
        Test t = await _context.Test.SingleAsync(t => t.Id == id);
        t.Questions = value.Questions.ToQuestionList();
        t.Duration = value.Duration;
        t.End = value.End;
        t.Scheduled = value.Scheduled;
        t.Name = value.Name;
        t.Password = value.Password;

        _context.Test.Update(t);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("submit")]
    public async Task<ActionResult> Submit([FromBody] TestSubmissionRequest submissionRequest)
    {
        string tokenString = Request.Headers["Authorization"].Single()!.Replace("Bearer ", "");
        var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
        int userId = Convert.ToInt32(token.Payload["userId"]);

        var newSubmission = _context.TestSubmission.Add(submissionRequest.ToSubmission(userId));
        await _context.SaveChangesAsync();

        return Created($"api/Test/submissions/{newSubmission.Entity.Id}", "");
    }

    // DELETE api/<TestController>/5
    [HttpDelete]
    public async Task<ActionResult> Delete([FromQuery] int id)
    {
        Test t = await _context.Test.SingleAsync(t => t.Id == id);

        _context.Test.Remove(t);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

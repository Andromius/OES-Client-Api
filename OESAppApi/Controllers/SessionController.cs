using Domain.Entities.Sessions;
using Domain.Entities.Users;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OESAppApi.Models;
using Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OESAppApi.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class SessionController : ControllerBase
{
    private readonly OESAppApiDbContext _context;
    private readonly ITokenService _tokenService;

    public SessionController(OESAppApiDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    // GET: api/<SessionController>
    [Authorize(Policy = "AdminPolicy")]
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<SessionController>/5
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SessionResponse>>> GetUserSessions()
    {
        string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var tokenHandler = new JwtSecurityTokenHandler();
        var t = tokenHandler.ReadJwtToken(token);
        int userId = Convert.ToInt32((string)t.Payload["userId"]);

        List<SessionResponse> response = await _context.Session
            .Where(s => s.UserId == userId)
            .Select(s => s.ToResponse(tokenHandler.ReadJwtToken(s.Token).ValidFrom))
            .ToListAsync();

        return response;
    }
}

using Domain.Entities.Sessions;
using Domain.Entities.Users;
using Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OESAppApi.Extensions;
using OESAppApi.Models;
using Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OESAppApi.Api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AdminPolicy")]
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<SessionController>/5
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SessionResponse>>> GetUserSessions()
    {
        List<SessionResponse> response = await _context.Session
            .Where(s => s.UserId == _tokenService.GetUserId(Request.ExtractToken()))
            .Select(s => s.ToResponse(_tokenService.ReadToken(s.Token).ValidFrom))
            .ToListAsync();

        return response;
    }
}

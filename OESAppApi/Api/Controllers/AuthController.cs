using Domain.Entities.Devices;
using Domain.Entities.Sessions;
using Domain.Entities.Users;
using Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using OESAppApi.Extensions;
using Persistence;

namespace OESAppApi.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ILogger<TestsController> _logger;
    private readonly OESAppApiDbContext _context;
    private readonly ITokenService _tokenService;

    public AuthController(ILogger<TestsController> logger, OESAppApiDbContext context, ITokenService tokenService)
    {
        _logger = logger;
        _context = context;
        _tokenService = tokenService;
    }

    //[HttpPost]
    //public async Task<ActionResult> AdminLogin([FromBody] LoginRequest loginRequest)
    //{
    //    User? user = await _context.User.SingleOrDefaultAsync(u => u.Username == loginRequest.Username);
    //    if (user is null)
    //    {
    //        return NotFound();
    //    }

    //    if (user.Role != UserRole.Admin || !PasswordService.CompareHash(loginRequest.Password, user.Password))
    //        return Unauthorized("Invalid password");
    //    HttpContext.Response.Headers.Add(new("Authorization", _tokenService.GenerateToken(user.Id, user.Role, DateTime.UtcNow)));
    //    return Redirect("/");
    //}

    [HttpPost]
    public async Task<ActionResult<UserAuthResponse>> Login([FromBody] LoginRequest loginRequest)
    {
        User? user = await _context.User.SingleOrDefaultAsync(u => u.Username == loginRequest.Username);
        if (user is null)
        {
            return NotFound();
        }

        if (!PasswordService.CompareHash(loginRequest.Password, user.Password))
            return Unauthorized("Invalid password");

        DevicePlatform devicePlatform = _context.DevicePlatform.Single(p => p.Id == loginRequest.DeviceRequest.PlatformId);
        string newToken = _tokenService.GenerateToken(user.Id, user.Role, DateTime.UtcNow);
        await _context.Session.AddAsync(new Session(loginRequest.DeviceRequest.Name, loginRequest.DeviceRequest.IsWeb, newToken, devicePlatform, user));
        await _context.SaveChangesAsync();
        return Ok(new UserAuthResponse(user.Id, user.FirstName, user.LastName, user.Username, newToken, user.Role));
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "StudentOrTeacherOrAdminPolicy")]
    [HttpPost]
    public async Task<ActionResult<UserAuthResponse>> TokenLogin()
    {
        string token = Request.ExtractToken();
        Session? session = await _context.Session.Include(s => s.DevicePlatform).SingleOrDefaultAsync(s => s.Token == token);
        if (session is null)
        {
            return NotFound();
        }

        _context.Session.Remove(session);

        User user = await _context.User.SingleAsync(u => u.Id == session.UserId);

        string newToken = _tokenService.GenerateToken(user.Id, user.Role, DateTime.UtcNow);
        await _context.Session.AddAsync(new Session(session.DeviceName, session.IsWeb, newToken, session.DevicePlatform, user));
        await _context.SaveChangesAsync();
        return Ok(new UserAuthResponse(user.Id, user.FirstName, user.LastName, user.Username, newToken, user.Role));
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "StudentOrTeacherOrAdminPolicy")]
    [HttpPost]
    public async Task<ActionResult> TokenLogout()
    {
        string token = Request.ExtractToken();

        await _context.Session.Where(s => s.Token == token).ExecuteDeleteAsync();

        return NoContent();
    }
}

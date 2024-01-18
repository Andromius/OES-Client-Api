using Domain.Entities.Devices;
using Domain.Entities.Sessions;
using Domain.Entities.Users;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using Persistence;

namespace OESAppApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ILogger<TestController> _logger;
    private readonly OESAppApiDbContext _context;
    private readonly ITokenService _tokenService;

    public AuthController(ILogger<TestController> logger, OESAppApiDbContext context, ITokenService tokenService)
    {
        _logger = logger;
        _context = context;
        _tokenService = tokenService;
    }

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

    [Authorize(Policy = "StudentOrTeacherOrAdminPolicy")]
    [HttpPost]
    public async Task<ActionResult<UserAuthResponse>> TokenLogin()
    {
        string token = HttpContext.Request.Headers["Authorization"].Single()!.Replace("Bearer ", "");
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

    [Authorize(Policy = "StudentOrTeacherOrAdminPolicy")]
    [HttpPost]
    public async Task<ActionResult> TokenLogout()
    {
        string token = HttpContext.Request.Headers["Authorization"].Single()!.Replace("Bearer ", "");

        await _context.Session.Where(s => s.Token == token).ExecuteDeleteAsync();

        return NoContent();
    }
}

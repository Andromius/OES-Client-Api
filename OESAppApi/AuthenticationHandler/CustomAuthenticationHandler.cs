using Domain.Entities.Sessions;
using Domain.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using System.Text.Encodings.Web;
using System.IdentityModel.Tokens.Jwt;

namespace OESAppApi.AuthenticationHandler;

public class CustomAuthenticationHandler : AuthenticationHandler<JwtBearerOptions>
{
    private readonly OESAppApiDbContext _context;
    private readonly ITokenService _tokenService;

    public CustomAuthenticationHandler(
        IOptionsMonitor<JwtBearerOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        OESAppApiDbContext context,
        ITokenService tokenService)
        : base(options, logger, encoder, clock)
    {
        _context = context;
        _tokenService = tokenService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {

        string token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
        if (Request.Path == "/signalr/quiz") 
        {
            token = Request.Query["access_token"]!;
        }

        if (string.IsNullOrEmpty(token))
            return AuthenticateResult.Fail("Token not present");

        Session? session = await _context.Session.SingleOrDefaultAsync(s => s.Token == token);
        if (session is null)
            return AuthenticateResult.Fail("Token not found");

        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var principal = await _tokenService.ValidateAsync(token);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
        catch (Exception ex)
        {
            _context.Session.Remove(session);
            await _context.SaveChangesAsync();
            return AuthenticateResult.Fail(ex.Message);
        }
    }

    protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        Response.StatusCode = 401;
        await Response.WriteAsync("Unauthorized");
    }
}

using Domain.Entities.Sessions;
using Domain.Entities.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services;

public interface ITokenService
{
    string GenerateToken(int userId, UserRole userRole, DateTime creationTime);
    Task<ClaimsPrincipal> GetPrincipal(string tokenString);
    int GetUserId(string tokenString);
    JsonWebToken ReadToken(string tokenString);
    Task<ClaimsPrincipal> ValidateAsync(string tokenString);
}

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly JsonWebTokenHandler _tokenHandler = new();
    private readonly TokenValidationParameters _validationParameters;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
        _validationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
        };
    }

    public string GenerateToken(int userId, UserRole userRole, DateTime creation)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new("userId", userId.ToString()),
                new(ClaimTypes.Role, userRole.ToString())
            }),
            IssuedAt = creation,
            Expires = DateTime.UtcNow.AddDays(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        return _tokenHandler.CreateToken(tokenDescriptor);
    }

    public int GetUserId(string tokenString)
    {
        var token = _tokenHandler.ReadJsonWebToken(tokenString);
        token.TryGetValue("userId", out int userId);
        return userId;
    }

    public async Task<ClaimsPrincipal> GetPrincipal(string tokenString)
    {
        var result = await _tokenHandler.ValidateTokenAsync(tokenString, _validationParameters);
        return new ClaimsPrincipal(result.ClaimsIdentity);
    }

    public JsonWebToken ReadToken(string tokenString)
    {
        return _tokenHandler.ReadJsonWebToken(tokenString);
    }

    public async Task<ClaimsPrincipal> ValidateAsync(string tokenString)
    {
        TokenValidationResult result = await _tokenHandler.ValidateTokenAsync(tokenString, _validationParameters);
        if (result.IsValid)
        {
            return new ClaimsPrincipal(result.ClaimsIdentity);
        }
        throw result.Exception;
    }
}

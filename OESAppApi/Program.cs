using Domain.Entities.Users;
using Domain.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OESAppApi.AuthenticationHandler;
using OESAppApi.Hubs;
using OESAppApi.Swagger;
using Persistence;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace OESAppApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Services.AddDbContext<OESAppApiDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            options.EnableSensitiveDataLogging();
            options.LogTo(Console.WriteLine);
        });
        builder.Services.AddSingleton<ITokenService, TokenService>();
        builder.Services.AddSingleton<CourseCodeGenerationService>();
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddScheme<JwtBearerOptions, CustomAuthenticationHandler>(JwtBearerDefaults.AuthenticationScheme, options => 
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, UserRole.Admin.ToString());
            });

            options.AddPolicy("TeacherOrAdminPolicy", policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, UserRole.Teacher.ToString(), UserRole.Admin.ToString());
            });

            options.AddPolicy("StudentOrTeacherOrAdminPolicy", policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, UserRole.Student.ToString(), UserRole.Teacher.ToString(), UserRole.Admin.ToString());
            });
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        builder.Services.AddSignalR();

        var app = builder.Build();
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<OESAppApiDbContext>();
            db.Database.EnsureCreated();
        }

        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
            app.UseSwagger();
            app.UseSwaggerUI();
        //}

        app.UseHttpsRedirection();

        app.UseCors("AllowAll");

        app.UseAuthentication();
        app.UseAuthorization();
        app.MapHub<TestingHub>("/testing", options =>
        {
            options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.LongPolling;
            options.LongPolling.PollTimeout = TimeSpan.FromSeconds(10);
        });

        app.MapControllers();

        app.Run();
    }
}
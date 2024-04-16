using Domain.Entities.Homeworks;
using Domain.Entities.Users;
using Domain.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OESAppApi.Api.Hubs;
using OESAppApi.Api.Swagger;
using OESAppApi.AuthenticationHandler;
using OESAppApi.Data;
using Persistence;
using Persistence.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;
using System.Security.Claims;
using System.Text;
using Domain.Entities.Courses;
using Newtonsoft.Json.Serialization;
using System.Text.Json;
using OESAppApi.Api.Services;

namespace OESAppApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Logging.ClearProviders();
        builder.Logging.SetMinimumLevel(LogLevel.Information);
        builder.Logging.AddConsole();
        builder.Services.AddDbContext<OESAppApiDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration["DATABASE_CONNECTION_STRING"], b => b.MigrationsAssembly("OESAppApi"));
            options.LogTo(Console.WriteLine, LogLevel.Warning);
        });
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddSingleton<WeatherForecastService>();
        builder.Services.AddSingleton<ITokenService, TokenService>();
        builder.Services.AddSingleton<CourseCodeGenerationService>();
        builder.Services.AddScoped<IHomeworkSubmissionAttachmentRepository, HomeworkSubmissionAttachmentRepository>(provider =>
            new(provider.GetRequiredService<OESAppApiDbContext>(), builder.Configuration["DATABASE_CONNECTION_STRING"]!)
        );
        builder.Services.AddScoped<ICourseRepository, CourseRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddSingleton<InMemoryQuizService>();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddQuickGridEntityFrameworkAdapter();
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
        builder.Services.AddAuthentication("Cookies").AddCookie(c =>
        {
            c.LoginPath = "/identity/account/login";
            c.LogoutPath = "/identity/account/logout";
		});
        builder.Services.Configure<CircuitOptions>(options =>
        {
            options.DetailedErrors = true;
        });
        builder.Services.AddScoped<TokenProvider>();
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
        builder.Services.AddSignalR().AddJsonProtocol(configuration =>
        {
            configuration.PayloadSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });
        builder.Services.AddHostedService<AnswerSimilarityCheckingService>();
        builder.Services.AddSingleton<BackgroundWorkerQueue>();
        var app = builder.Build();
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<OESAppApiDbContext>();
            db.Database.Migrate();
        }

        //if (!app.Environment.IsDevelopment())
        //{
        //    app.UseExceptionHandler("/Error");
        //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        //    app.UseHsts();
        //}

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseCors("AllowAll");
        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapHub<QuizHub>("/signalr/quiz");

        app.MapBlazorHub(options => 
        {
            options.CloseOnAuthenticationExpiration = true;
        });
        app.MapFallbackToPage("/_Host");
        app.MapControllers();
        app.MapGet("/api", () => Results.StatusCode(418));
        app.Use(async (context, next) =>
        {
            Console.WriteLine(context.Request.Path);
            await next();
        });
        app.Run();
    }
}
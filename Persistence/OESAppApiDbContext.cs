using Domain.Entities.Courses;
using Domain.Entities.Devices;
using Domain.Entities.Sessions;
using Domain.Entities.Tests;
using Domain.Entities.Tests.Answers;
using Domain.Entities.Tests.Questions;
using Domain.Entities.Users;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections;

namespace Persistence;

public class OESAppApiDbContext : DbContext
{
    public DbSet<Test> Test { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Course> Course { get; set; } 
    public DbSet<CourseXUser> CourseXUser { get; set; }
    public DbSet<Question> Question { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<DevicePlatform> DevicePlatform { get; set; }
    public DbSet<Session> Session { get; set; }
    public DbSet<TestSubmission> TestSubmission { get; set; }
    public DbSet<Answer> Answer { get; set; }
    public OESAppApiDbContext() { }
    public OESAppApiDbContext(DbContextOptions options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Session>().HasKey(s => s.Token);
        
        modelBuilder.Entity<User>().HasData(SeedUsers());
        
        modelBuilder.Entity<Course>().HasData(SeedCourses());
        
        modelBuilder.Entity<DevicePlatform>().HasData(SeedDevicePlatforms());
        
        modelBuilder.Entity<CourseXUser>().HasKey(x => new { x.CourseId, x.UserId });
        modelBuilder.Entity<CourseXUser>().HasData(SeedCourseXUser());
        
        modelBuilder.Entity<Test>().HasData(SeedTests());
        
        modelBuilder.Entity<Question>().HasKey(x => new { x.Id, x.TestId });
        modelBuilder.Entity<Question>().HasData(SeedQuestions());
        
        modelBuilder.Entity<Option>().HasKey("Id", "QuestionId", "QuestionTestId");
        modelBuilder.Entity<Option>().HasData(SeedOptions());

        modelBuilder.Entity<Answer>().HasKey(x => new {x.Id, x.QuestionId, x.TestSubmissionId});
        //modelBuilder.Entity<User>().HasKey(x => x.Id);
        base.OnModelCreating(modelBuilder);
    }

    private static IEnumerable<Option> SeedOptions()
    {
        List<Option> options = new()
        {
            new()
            { 
                Id = 1,
                Text = "Opt A",
                QuestionId = 1,
                QuestionTestId = 1,
                Points = 3
            },
            new()
            {
                Id = 2,
                Text = "Opt B",
                QuestionId = 1,
                QuestionTestId = 1,
                Points = 0
            },
            new()
            {
                Id = 3,
                Text = "Opt C",
                QuestionId = 1,
                QuestionTestId = 1,
                Points = 0
            },
            new()
            {
                Id = 1,
                Text = "Opt A",
                QuestionId = 2,
                QuestionTestId = 1,
                Points = 3
            },
            new()
            {
                Id = 2,
                Text = "Opt B",
                QuestionId = 2,
                QuestionTestId = 1,
                Points = -3
            },
            new()
            {
                Id = 3,
                Text = "Opt C",
                QuestionId = 2,
                QuestionTestId = 1,
                Points = 3
            }
        };
        return options;
    }

    private static IEnumerable<DevicePlatform> SeedDevicePlatforms()
    {
        List<DevicePlatform> platforms = new();
        foreach (var platform in Enum.GetValues<EDevicePlatform>())
        {
            platforms.Add(new DevicePlatform(platform, platform.ToString()));
        }
        return platforms;
    }

    private static IEnumerable<Course> SeedCourses()
    {
        List<Course> courses = new()
        {
            new Course(1, "Python", "P", "You will learn basic skill about python", null, "ABC12"),
            new Course(2, "English", "E", "You will learn basic skill about english", "0xffec6337", "ABC21"),
            new Course(3, "Java", "J", "You will learn basic skill about java", "0xff00ff00", "CBA12"),
        };
        return courses;
    }

    private static IEnumerable<CourseXUser> SeedCourseXUser()
    {
        List<CourseXUser> courseXUsers = new()
        {
            new CourseXUser(1, 3, CourseEnum.Attendant),
            new CourseXUser(2, 3, CourseEnum.Attendant),
            new CourseXUser(3, 3, CourseEnum.Attendant),
            new CourseXUser(1, 2, CourseEnum.Teacher),
            new CourseXUser(2, 2, CourseEnum.Teacher),
            new CourseXUser(3, 2, CourseEnum.Teacher),
        };
        return courseXUsers;
    }

    private static IEnumerable<Test> SeedTests()
    {
        List<Test> tests = new()
        {
            new Test(1, "Write 100x hello!", DateTime.UtcNow, true, 1, 3, DateTime.UtcNow.AddMinutes(30), DateTime.UtcNow.AddHours(3), 30*60, "password", 3)
        };

        return tests;
    }

    private static IEnumerable<Question> SeedQuestions()
    {
        List<Question> questions = new()
        {
            new Question()
            {
                Id = 1,
                Description = "What is hello?",
                Name = "Question 1",
                TestId = 1,
                Points = 3,
                Type = "pick-one"
            },
            new Question()
            {
                Id = 2,
                Description = "Pick many question.",
                Name = "Question 2",
                TestId = 1,
                Points = 6,
                Type = "pick-many"
            },
            new Question()
            {
                Id = 3,
                Description = "Write hello.",
                Name = "Question 3",
                TestId = 1,
                Points = 10,
                Type = "open"
            }
        };
        return questions;
    }

    private static IEnumerable<User> SeedUsers()
    {
        List<User> users = new()
        {
            new User(1, "Admin", "Doe", "admin", PasswordService.GetHash("admin"), UserRole.Admin),
            new User(2, "Teacher", "Doe", "teacher", PasswordService.GetHash("teacher"), UserRole.Teacher),
            new User(3, "Student", "Doe", "student", PasswordService.GetHash("student"), UserRole.Student),
            new User(4, "Alice", "Doe", "teachMaster", PasswordService.GetHash("teacher"), UserRole.Teacher),
            new User(5, "Bob", "Doe", "eduGuru", PasswordService.GetHash("teacher"), UserRole.Teacher),
            new User(6, "Charlie", "Doe", "profLearner", PasswordService.GetHash("teacher"), UserRole.Teacher),
            new User(7, "David", "Doe", "learnSculptor", PasswordService.GetHash("teacher"), UserRole.Teacher),
            new User(8, "Ella", "Doe", "knowledgeNinja", PasswordService.GetHash("teacher"), UserRole.Teacher),
            new User(9, "Frank", "Doe", "scholarSavvy", PasswordService.GetHash("teacher"), UserRole.Teacher),
            new User(10, "Grace", "Doe", "eduMaestro", PasswordService.GetHash("teacher"), UserRole.Teacher),
            new User(11, "Hannah", "Smith", "brainyTutor", PasswordService.GetHash("teacher"), UserRole.Teacher),
            new User(12, "Isaac", "Johnson", "wisdomWhiz", PasswordService.GetHash("teacher"), UserRole.Teacher)
        };

        return users;
    }
}
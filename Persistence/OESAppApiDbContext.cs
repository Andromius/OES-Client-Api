using Domain.Entities.Courses;
using Domain.Entities.Devices;
using Domain.Entities.Homeworks;
using Domain.Entities.Notes;
using Domain.Entities.Questions;
using Domain.Entities.Questions.Options;
using Domain.Entities.Quizzes;
using Domain.Entities.Sessions;
using Domain.Entities.Tests;
using Domain.Entities.Tests.Answers;
using Domain.Entities.Tests.Submissions;
using Domain.Entities.Tests.Submissions.Reviews;
using Domain.Entities.UserQuizzes;
using Domain.Entities.Users;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Reflection;

namespace Persistence;

public class OESAppApiDbContext : DbContext
{
    public DbSet<Test> Test { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Course> Course { get; set; }
    public DbSet<CourseItem> CourseItem { get; set; }
    public DbSet<CourseXUser> CourseXUser { get; set; }
    public DbSet<Question> Question { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<DevicePlatform> DevicePlatform { get; set; }
    public DbSet<Session> Session { get; set; }
    public DbSet<TestSubmission> TestSubmission { get; set; }
    public DbSet<Answer> Answer { get; set; }
    public DbSet<Note> Note { get; set; }
    public DbSet<Homework> Homework { get; set; }
    public DbSet<HomeworkSubmission> HomeworkSubmission { get; set; }
    public DbSet<HomeworkSubmissionAttachment> HomeworkSubmissionAttachment { get; set; }
    public DbSet<HomeworkScore> HomeworkScore { get; set; } 
    public DbSet<TestSubmissionReview> TestSubmissionReview { get; set; }
    public DbSet<Quiz> Quiz { get; set; }
    public DbSet<UserQuiz> UserQuiz { get; set; }
    public DbSet<AnswerSimilarity> AnswerSimilarity { get; set; }
    public OESAppApiDbContext() { }
    public OESAppApiDbContext(DbContextOptions options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
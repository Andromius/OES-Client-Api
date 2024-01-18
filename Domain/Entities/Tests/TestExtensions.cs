using Domain.Entities.Courses;
using Domain.Entities.Tests.Answers;
using Domain.Entities.Tests.Questions;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Tests;

public static class TestExtensions
{
    public static Test ToTest(this TestRequest testRequest, User user, Course course)
    {
        Test t = new()
        {
            Name = testRequest.Name,
            Created = testRequest.Created,
            IsVisible = testRequest.IsVisible,
            User = user,
            UserId = user.Id,
            Course = course,
            CourseId = course.Id,
            Scheduled = testRequest.Scheduled,
            End = testRequest.End,
            Duration = testRequest.Duration,
            Password = testRequest.Password,
            MaxAttempts = testRequest.MaxAttempts
        };
        t.Questions = testRequest.Questions.Select(q => q.ToQuestion()).ToList();
        t.Questions.ForEach(q => q.Id = t.Questions.IndexOf(q));
        return t;
    }

    public static TestResponse ToResponse(this Test test, CourseEnum who) => new(
        nameof(Test),
        test.Id,
        test.Name,
        test.Created,
        test.UserId,
        test.IsVisible,
        test.Scheduled,
        test.End,
        test.Duration,
        test.MaxAttempts,
        test.Questions.Select(q => q.ToResponse(who)).ToList());

    public static TestSubmission ToSubmission(this TestSubmissionRequest request, int userId)
    {
        List<Answer> answers = request.Answers.Select(ar => ar.ToAnswer(request.TestId)).ToList();
        return new(userId, request.TestId, answers, Common.ESubmissionStatus.Submitted, DateTime.UtcNow);
    }
}

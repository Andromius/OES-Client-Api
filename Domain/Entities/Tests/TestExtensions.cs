using Domain.Entities.Courses;
using Domain.Entities.Questions;
using Domain.Entities.Tests.Answers;
using Domain.Entities.Tests.Submissions;
using Domain.Entities.Users;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Tests;

public static class TestExtensions
{
    public static Test ToTest(this TestRequest testRequest, int userId, int courseId)
    {
        Test t = new()
        {
            Name = testRequest.Name,
            Created = testRequest.Created,
            IsVisible = testRequest.IsVisible,
            UserId = userId,
            CourseId = courseId,
            Scheduled = testRequest.Scheduled,
            End = testRequest.End,
            Duration = testRequest.Duration,
            Password = testRequest.Password,
            MaxAttempts = testRequest.MaxAttempts,
            Questions = testRequest.Questions.Select(q => q.ToQuestion()).ToList()
        };
        return t;
    }

    public static TestResponse ToResponse(this Test test, CourseEnum courseParticipant) => new(
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
        courseParticipant is CourseEnum.Teacher ? test.Password : null,
        test.Questions.Select(q => q.ToResponse(courseParticipant)).ToList());

    public static TestSubmission ToSubmission(this TestSubmissionRequest request, int userId)
    {
        List<Answer> answers = request.Answers.Select(ar => ar.ToAnswer()).ToList();
        return new(userId, request.TestId, answers, Common.ESubmissionStatus.Submitted, DateTime.UtcNow);
    }

    public static TestSubmissionResponse ToResponse(this TestSubmission submission)
    {
        return new TestSubmissionResponse()
        {
            Id = submission.Id,
            GradedAt = submission.GradedAt,
            SubmittedAt = submission.SubmittedAt,
            Status = submission.Status.ToString(),
            TestId = submission.TestId,
            TotalPoints = submission.TotalPoints
        };
    }

    public static TestInfo ToInfo(this Test test, List<TestSubmission> submissions)
        => new(test.Name, test.End, test.Duration, test.MaxAttempts, !test.Password.IsNullOrEmpty(), submissions.Select(s => s.ToAttempt()));

    public static TestAttempt ToAttempt(this TestSubmission submission)
        => new(submission.Reviews.Count > 0 ? submission.Reviews.Sum(r => r.Points) : (submission.TotalPoints ?? 0), submission.Status.ToString(), submission.SubmittedAt);
}

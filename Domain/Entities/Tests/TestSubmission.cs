using Domain.Entities.Common;
using Domain.Entities.Tests.Answers;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Tests;
public class TestSubmission
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TestSubmission() { }
    public TestSubmission(int userId, int testId, List<Answer> answers, ESubmissionStatus status, DateTime submittedAt)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        UserId = userId;
        TestId = testId;
        Answers = answers;
        Status = status;
        SubmittedAt = submittedAt;
        GradedAt = null;
        TotalPoints = null;
    }

    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int TestId { get; set; }
    public Test Test { get; set; }
    public List<Answer> Answers { get; set; }
    public ESubmissionStatus Status { get; set; }
    public DateTime SubmittedAt { get; set; }
    public DateTime? GradedAt { get; set; }
    public int? TotalPoints { get; set; }
}

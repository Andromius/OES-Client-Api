using Domain.Entities.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Tests.Submissions.Reviews;
public class TestSubmissionReview
{
    public Question Question { get; set; }
    public int QuestionId { get; set; }
    public TestSubmission Submission { get; set; }
    public int SubmissionId { get; set; }
    public int Points { get; set; }
}

using Domain.Entities.Questions;
using Domain.Entities.Tests.Submissions;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Tests.Answers;
public class AnswerSimilarity
{
    public int QuestionId { get; set; }
    public Question Question { get; set; }
    public int SubmissionId { get; set; }
    public TestSubmission Submission { get; set; }
    public int CheckAgainstSubmissionId { get; set; }
    public TestSubmission CheckAgainstSubmission { get; set; }
    public double Similarity { get; set; }
}

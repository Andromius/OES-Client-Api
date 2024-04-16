using Domain.Entities.Questions;
using Domain.Entities.Tests.Submissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Tests.Answers;
public class Answer
{
    public Answer(int id, string text, int questionId)
    {
        Id = id;
        Text = text;
        QuestionId = questionId;
    }

    public int Id { get; set; }
    public string Text { get; set; }
    public int QuestionId { get; set; }
    public Question Question { get; set; }
    public int TestSubmissionId { get; set; }
    public TestSubmission TestSubmission { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Tests.Answers;
public class AnswerRequest
{
    public int QuestionId { get; set; }
    public int Id { get; set; }
    public string Text { get; set; }
}

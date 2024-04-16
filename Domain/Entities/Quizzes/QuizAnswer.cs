using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Quizzes;
public class QuizAnswer
{
    public int OptionId { get; set; }
    public int QuestionId { get; set; }
    public DateTime SubmittedAt { get; set; }
}

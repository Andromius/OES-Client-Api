using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Tests.Answers;
public static class AnswerExtensions
{
    public static Answer ToAnswer(this AnswerRequest request) => new(request.Id, request.Text, request.QuestionId);
}

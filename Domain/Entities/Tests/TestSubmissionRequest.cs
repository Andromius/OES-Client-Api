using Domain.Entities.Tests.Answers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Tests;
public class TestSubmissionRequest
{
    public int TestId { get; set; }
    public List<AnswerRequest> Answers { get; set; }
}

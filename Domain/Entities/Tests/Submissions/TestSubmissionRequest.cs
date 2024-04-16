using Domain.Entities.Tests.Answers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Tests.Submissions;
public record TestSubmissionRequest(int TestId, List<AnswerRequest> Answers);

using Domain.Entities.Common;
using Domain.Entities.Tests.Answers;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Tests.Submissions;
public class TestSubmissionResponse
{
    public int Id { get; set; }
    public int TestId { get; set; }
    public string Status { get; set; } = null!;
    public DateTime SubmittedAt { get; set; }
    public DateTime? GradedAt { get; set; }
    public int? TotalPoints { get; set; }
}

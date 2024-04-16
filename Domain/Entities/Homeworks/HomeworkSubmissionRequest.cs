using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Homeworks;
public class HomeworkSubmissionRequest
{
    public string? Text { get; set; }
    public IFormFileCollection? FormFiles { get; set; }
}

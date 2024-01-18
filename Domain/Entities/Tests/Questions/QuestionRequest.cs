using Domain.Entities.Tests.Answers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.Tests.Questions;

public class QuestionRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<OptionRequest> Options { get; set; }
    public int Points { get; set; }
    public string Type { get; set; }
}

using Domain.Entities.Tests;
using Domain.Entities.Tests.Answers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.Tests.Questions;

public class Question
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Option> Options { get; set; } = new();
    public int Points { get; set; }
    public string Type { get; set; }
    public int TestId { get; set; }
    public Test Test { get; set; }
}

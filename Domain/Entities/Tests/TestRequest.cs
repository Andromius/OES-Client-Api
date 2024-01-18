using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Courses;
using Domain.Entities.Tests.Questions;
using Domain.Entities.Users;

namespace Domain.Entities.Tests;

public class TestRequest
{
    public string Name { get; set; }
    public DateTime Created { get; set; }
    public bool IsVisible { get; set; }
    public int CreatedById { get; set; }
    public DateTime Scheduled { get; set; }
    public DateTime End { get; set; }
    public int Duration { get; set; }
    public string Password { get; set; }
    public int MaxAttempts { get; set; }
    public List<QuestionRequest> Questions { get; set; } = new();
}

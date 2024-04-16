using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Courses;
using Domain.Entities.Questions;
using Domain.Entities.Users;

namespace Domain.Entities.Tests;

public record TestRequest(
    string Name,
    DateTime Created,
    bool IsVisible,
    int CreatedById,
    DateTime Scheduled,
    DateTime End,
    int Duration,
    string Password,
    int MaxAttempts,
    List<QuestionRequest> Questions);

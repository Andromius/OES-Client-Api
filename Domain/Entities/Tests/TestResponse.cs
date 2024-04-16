using Domain.Entities.Courses;
using Domain.Entities.Questions;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Tests;

public record TestResponse(string Type,
                           int Id,
                           string Name,
                           DateTime Created,
                           int CreatedById,
                           bool IsVisible,
                           DateTime Scheduled,
                           DateTime End,
                           int Duration,
                           int MaxAttempts,
                           string? Password,
                           List<QuestionResponse> Questions) : CourseItemResponse(Type, Id, Name, Created, CreatedById, IsVisible);

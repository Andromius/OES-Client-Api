using Domain.Entities.Courses;
using Domain.Entities.Tests.Questions;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Tests;

public class TestResponse : CourseItemResponse
{
    public DateTime Scheduled { get; set; }
    public DateTime End { get; set; }
    public int Duration { get; set; }
    public int MaxAttempts { get; set; }
    public List<QuestionResponse> Questions { get; set; } = new();
    public TestResponse(string type,
                        int id,
                        string name,
                        DateTime created,
                        int createdById,
                        bool isVisible,
                        DateTime scheduled,
                        DateTime end,
                        int duration,
                        int maxAttempts,
                        List<QuestionResponse> questions) : base(type, id, name, created, createdById, isVisible)
    {
        Scheduled = scheduled;
        End = end;
        Duration = duration;
        MaxAttempts = maxAttempts;
        Questions = questions;
    }
}

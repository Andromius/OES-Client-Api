using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Courses;
using Domain.Entities.Tests.Questions;
using Domain.Entities.Users;

namespace Domain.Entities.Tests;

public class Test : CourseItem
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Test() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Test(
        int id,
        string name,
        DateTime created,
        bool isVisible,
        int userId,
        int courseId,
        DateTime scheduled,
        DateTime end,
        int duration,
        string password,
        int maxAttempts) : base(id, name, created, isVisible, userId, courseId)
    {
        Scheduled = scheduled;
        End = end;
        Duration = duration;
        Password = password;
        MaxAttempts = maxAttempts;
    }

    public DateTime Scheduled { get; set; }
    public DateTime End { get; set; }
    public int Duration { get; set; }
    public string Password { get; set; }
    public int MaxAttempts { get; set; }
    public List<Question> Questions { get; set; } = new();
}

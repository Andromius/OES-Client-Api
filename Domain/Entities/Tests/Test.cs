using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Common;
using Domain.Entities.Courses;
using Domain.Entities.Questions;
using Domain.Entities.Tests.Submissions;
using Domain.Entities.Users;

namespace Domain.Entities.Tests;

public class Test : Schedulable
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Test() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Test(
        string name,
        DateTime created,
        bool isVisible,
        int userId,
        int courseId,
        DateTime scheduled,
        DateTime end,
        int duration,
        string password,
        int maxAttempts) : base(name, created, isVisible, userId, courseId, scheduled, end)
    {
        Duration = duration;
        Password = password;
        MaxAttempts = maxAttempts;
    }
    public int Duration { get; set; }
    public string Password { get; set; }
    public int MaxAttempts { get; set; }
    public List<TestSubmission> Submissions { get; set; } = new();
}

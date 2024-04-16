using Domain.Entities.Common;
using Domain.Entities.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Homeworks;
public class Homework : Schedulable
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Homework()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public Homework(
        string name,
        DateTime created,
        bool isVisible,
        int userId,
        int courseId,
        DateTime scheduled,
        DateTime end,
        string task) : base(name, created, isVisible, userId, courseId, scheduled, end)
    {
        Task = task;
    }

    public string Task { get; set; }
    public List<HomeworkSubmission> Submissions { get; set; } = [];
    public List<HomeworkScore> Scores { get; set; } = [];
}

using Domain.Entities.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common;
public abstract class Schedulable : CourseItem
{
    protected Schedulable() : base()
    {
    }

    protected Schedulable(
        string name,
        DateTime created,
        bool isVisible,
        int userId,
        int courseId,
        DateTime scheduled,
        DateTime end) : base(name, created, isVisible, userId, courseId)
    {
        Scheduled = scheduled;
        End = end;
    }

    public DateTime Scheduled { get; set; }
    public DateTime End { get; set; }
}

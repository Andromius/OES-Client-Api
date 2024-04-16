using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Homeworks;
public static class HomeworkExtensions
{
    public static Homework ToHomework(this HomeworkRequest r, int courseId, int userId)
        => new(r.Name, r.Created, r.IsVisible, userId, courseId, r.Scheduled, r.End, r.Task);

    public static HomeworkResponse ToResponse(this Homework h)
        => new(nameof(Homework), h.Id, h.Name, h.Created, h.UserId, h.IsVisible, h.Task, h.End, h.Scheduled);

    public static Homework UpdateFromRequest(this Homework h, HomeworkRequest r)
    {
        h.Name = r.Name;
        h.Created = r.Created;
        h.IsVisible = r.IsVisible;
        h.Scheduled = r.Scheduled;
        h.End = r.End;
        h.Task = r.Task;
        return h;
    }
}

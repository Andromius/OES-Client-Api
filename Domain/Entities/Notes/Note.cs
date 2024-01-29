using Domain.Entities.Courses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Notes;
public class Note : CourseItem
{
    public Note()
    {
    }

    public Note(int id, string name, DateTime created, bool isVisible, int userId, int courseId) : base(id, name, created, isVisible, userId, courseId)
    {
    }

    public string Data { get; set; }
}

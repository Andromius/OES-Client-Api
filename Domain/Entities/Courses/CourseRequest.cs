using Domain.Entities.Tests;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Courses;

public class CourseRequest
{
    [MinLength(1)]
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string Description { get; set; }
    public string? Color { get; set; }
    public List<int> TeacherIds { get; set; } = new();
    public List<int> AttendantIds { get; set; } = new();
}

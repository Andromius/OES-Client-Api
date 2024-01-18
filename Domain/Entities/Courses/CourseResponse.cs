using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Courses;

public class CourseResponse
{
    public CourseResponse(int id, string name, string shortName, string description, string? color)
    {
        Id = id;
        Name = name;
        ShortName = shortName;
        Description = description;
        Color = color;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string Description { get; set; }
    public string? Color { get; set; }
}

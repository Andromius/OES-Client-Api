using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Courses;

public class CourseItemResponse
{
    public CourseItemResponse(string type, int id, string name, DateTime created, int createdById, bool isVisible)
    {
        Type = type;
        Id = id;
        Name = name;
        Created = created;
        CreatedById = createdById;
        IsVisible = isVisible;
    }

    public string Type { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Created { get; set; }
    public int CreatedById { get; set; }
    public bool IsVisible { get; set; }
}

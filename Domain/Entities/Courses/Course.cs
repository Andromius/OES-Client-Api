using Domain.Entities.Tests;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Courses;

public class Course
{
    public Course(int id, string name, string shortName, string description, string? color, string code)
    {
        Id = id;
        Name = name;
        ShortName = shortName;
        Description = description;
        Color = color;
        Code = code;
    }

    public Course(string name, string shortName, string description, string? color, string code)
    {
        Name = name;
        ShortName = shortName;
        Description = description;
        Color = color;
        Code = code;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Course() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string Description { get; set; }
    public string? Color { get; set; }
    public string Code { get; set; }
    public List<Test> Tests { get; set; } = new();
    public List<CourseXUser> Users { get; set; } = new();
}

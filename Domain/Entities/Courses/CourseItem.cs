using Domain.Entities.Common;
using Domain.Entities.Questions;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Courses;

public abstract class CourseItem
{
    public CourseItem() { }

    protected CourseItem(string name, DateTime created, bool isVisible, int userId, int courseId)
    {
        Name = name;
        Created = created;
        IsVisible = isVisible;
        UserId = userId;
        CourseId = courseId;
    }

    public int Id { get; set; }
    public ECourseItemType CourseItemType { get; set; }
    public string Name { get; set; }
    public DateTime Created { get; set; }
    public bool IsVisible { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }
    public List<Question> Questions { get; set; } = new();
}

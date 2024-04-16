using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Courses;

public static class CourseExtensions
{
    public static CourseResponse ToResponse(this Course c)
    {
        return new CourseResponse(c.Id, c.Name, c.ShortName, c.Description, c.Color);
    }

    public static CourseItemResponse ToItemResponse(this CourseItem ci)
    {
        return new CourseItemResponse(ci.CourseItemType.ToString(), ci.Id, ci.Name, ci.Created, ci.UserId, ci.IsVisible);
    }

    public static Course ToCourse(this CourseRequest c, CourseCodeGenerationService codeGenerationService)
    {
        return new Course(c.Name, c.ShortName, c.Description, c.Color, codeGenerationService.GenerateUniqueCode());
    }

    public static void UpdateFromRequest(this Course c, CourseRequest request)
    {
        c.Description = request.Description;
        c.ShortName = request.ShortName;
        c.Name = request.Name;
        c.Color = request.Color;
        if (request.TeacherIds.Count > 0)
        {
            c.Users.RemoveAll(u => u.UserRole == CourseEnum.Teacher && !request.TeacherIds.Contains(u.UserId));
            AddMissingUsers(c, request.TeacherIds, CourseEnum.Teacher);
        }
        
        c.Users.RemoveAll(u => u.UserRole == CourseEnum.Attendant && !request.AttendantIds.Contains(u.UserId));
        AddMissingUsers(c, request.AttendantIds, CourseEnum.Attendant);
    }

    private static void AddMissingUsers(Course course, List<int> userIds, CourseEnum courseRole)
    {
        foreach (var id in userIds)
        {
            if (!course.Users.Exists(u => u.UserId == id))
                course.Users.Add(new (course.Id, id, courseRole));
        }
    }

}

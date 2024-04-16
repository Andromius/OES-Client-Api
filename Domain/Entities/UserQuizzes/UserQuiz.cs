using Domain.Entities.Common;
using Domain.Entities.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserQuizzes;
public class UserQuiz : CourseItem
{
    public UserQuiz()
    {
    }

    public UserQuiz(string name, DateTime created, bool isVisible, int userId, int courseId) : base(name, created, isVisible, userId, courseId)
    {
    }
}

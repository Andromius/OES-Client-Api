using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Courses;
public interface ICourseRepository
{
    Task<Result<CourseEnum>> GetUserCourseRoleAsync(int courseId, int userId);
}

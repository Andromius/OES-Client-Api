using Domain.Entities.Common;
using Domain.Entities.Courses;
using Domain.Entities.Tests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;
public class CourseRepository : ICourseRepository
{
    private readonly OESAppApiDbContext _context;

    public CourseRepository(OESAppApiDbContext context)
    {
        _context = context;
    }

    public async Task<Result<CourseEnum>> GetUserCourseRoleAsync(int courseId, int userId)
    {
        CourseEnum? userCourseRole =  await _context.CourseXUser
            .Where(cxu => cxu.UserId == userId && cxu.CourseId == courseId)
            .Select(cxu => new CourseEnum?(cxu.UserRole))
            .SingleOrDefaultAsync();

        if (userCourseRole is null)
            return Result<CourseEnum>.Failure();
        return Result<CourseEnum>.Success(userCourseRole.Value);
    }
}

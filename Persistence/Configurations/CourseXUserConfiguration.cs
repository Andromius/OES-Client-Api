using Domain.Entities.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;
public class CourseXUserConfiguration : IEntityTypeConfiguration<CourseXUser>
{
    public void Configure(EntityTypeBuilder<CourseXUser> builder)
    {
        builder.HasKey(x => new { x.CourseId, x.UserId });
        builder.HasData(SeedCourseXUser());
    }

    private static IEnumerable<CourseXUser> SeedCourseXUser()
    {
        List<CourseXUser> courseXUsers = new()
        {
            new CourseXUser(1, 3, CourseEnum.Attendant),
            new CourseXUser(2, 3, CourseEnum.Attendant),
            new CourseXUser(3, 3, CourseEnum.Attendant),
            new CourseXUser(1, 2, CourseEnum.Teacher),
            new CourseXUser(2, 2, CourseEnum.Teacher),
            new CourseXUser(3, 2, CourseEnum.Teacher),
        };
        return courseXUsers;
    }
}

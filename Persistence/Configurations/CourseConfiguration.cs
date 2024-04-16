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
public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasData(SeedCourses());
    }

    private static IEnumerable<Course> SeedCourses()
    {
        List<Course> courses = new()
        {
            new Course(1, "Python", "P", "You will learn basic skill about python", null, "ABC12"),
            new Course(2, "English", "E", "You will learn basic skill about english", "0xffec6337", "ABC21"),
            new Course(3, "Java", "J", "You will learn basic skill about java", "0xff00ff00", "CBA12"),
        };
        return courses;
    }
}

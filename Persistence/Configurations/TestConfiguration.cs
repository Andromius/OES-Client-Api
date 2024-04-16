using Domain.Entities.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;
public class TestConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.HasData(SeedTests());
    }

    private static IEnumerable<Test> SeedTests()
    {
        List<Test> tests =
        [
            new Test("Write 100x hello!", DateTime.UtcNow, true, 1, 3, DateTime.UtcNow.AddMinutes(30), DateTime.UtcNow.AddHours(3), 30*60, "password", 3)
            {
                Id = 1,
                CourseItemType = Domain.Entities.Common.ECourseItemType.Test
            }
        ];

        return tests;
    }
}

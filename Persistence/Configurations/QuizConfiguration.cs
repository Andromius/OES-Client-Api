using Domain.Entities.Quizzes;
using Domain.Entities.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;
public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        builder.HasData(SeedQuizzes());
    }

    private IEnumerable<Quiz> SeedQuizzes()
    {
        List<Quiz> quizzes =
        [
            new Quiz("AMAZING QUIZ", DateTime.UtcNow, true, 1, 3, DateTime.UtcNow.AddMinutes(30), DateTime.UtcNow.AddHours(3))
            {
                Id = 2,
                CourseItemType = Domain.Entities.Common.ECourseItemType.Test
            }
        ];

        return quizzes;
    }
}

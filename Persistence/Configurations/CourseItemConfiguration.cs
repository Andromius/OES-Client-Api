using Domain.Entities.Courses;
using Domain.Entities.Homeworks;
using Domain.Entities.Notes;
using Domain.Entities.Quizzes;
using Domain.Entities.Tests;
using Domain.Entities.UserQuizzes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;
public class CourseItemConfiguration : IEntityTypeConfiguration<CourseItem>
{
    public void Configure(EntityTypeBuilder<CourseItem> builder)
    {
        builder.HasDiscriminator(x => x.CourseItemType)
            .HasValue<Test>(Domain.Entities.Common.ECourseItemType.Test)
            .HasValue<Note>(Domain.Entities.Common.ECourseItemType.Note)
            .HasValue<Homework>(Domain.Entities.Common.ECourseItemType.Homework)
            .HasValue<CourseItem>(Domain.Entities.Common.ECourseItemType.Item)
            .HasValue<Quiz>(Domain.Entities.Common.ECourseItemType.Quiz)
            .HasValue<UserQuiz>(Domain.Entities.Common.ECourseItemType.UserQuiz);
        builder.Property(x => x.CourseItemType)
            .HasColumnName("CourseItemType");
    }
}

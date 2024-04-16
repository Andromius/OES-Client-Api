using Domain.Entities.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;
public class QuestionConfigruation : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasData(SeedQuestions());
    }

    private static IEnumerable<Question> SeedQuestions()
    {
        List<Question> questions = new()
        {
            new Question()
            {
                Id = 1,
                Description = "What is hello?",
                Name = "Question 1",
                ItemId = 1,
                Points = 3,
                Type = QuestionType.PickOne,
            },
            new Question()
            {
                Id = 2,
                Description = "Pick many question.",
                Name = "Question 2",
                ItemId = 1,
                Points = 6,
                Type = QuestionType.PickMany,
            },
            new Question()
            {
                Id = 3,
                Description = "Write hello.",
                Name = "Question 3",
                ItemId = 1,
                Points = 10,
                Type = QuestionType.Open,
            },
            new Question()
            {
                Id = 4,
                Description = "Write hello.",
                Name = "Question YESNO",
                ItemId = 2,
                Points = 1,
                Type = QuestionType.PickOne,
            },
            new Question()
            {
                Id = 5,
                Description = "Kaja je dobry programator",
                Name = "Question YESNO",
                ItemId = 2,
                Points = 4,
                Type = QuestionType.PickMany,
            }
        };
        return questions;
    }
}

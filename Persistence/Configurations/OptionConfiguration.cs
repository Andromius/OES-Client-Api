using Domain.Entities.Questions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;
public class OptionConfiguration : IEntityTypeConfiguration<Option>
{
    public void Configure(EntityTypeBuilder<Option> builder)
    {
        builder.HasKey(x => new { x.Id, x.QuestionId });
        builder.HasData(SeedOptions());
    }

    private static IEnumerable<Option> SeedOptions()
    {
        List<Option> options = new()
        {
            new()
            {
                Id = 1,
                Text = "Opt A",
                QuestionId = 1,
                Points = 3,
            },
            new()
            {
                Id = 2,
                Text = "Opt B",
                QuestionId = 1,
                Points = 0,
            },
            new()
            {
                Id = 3,
                Text = "Opt C",
                QuestionId = 1,
                Points = 0,
            },
            new()
            {
                Id = 1,
                Text = "Opt A",
                QuestionId = 2,
                Points = 3,
            },
            new()
            {
                Id = 2,
                Text = "Opt B",
                QuestionId = 2,
                Points = -3,
            },
            new()
            {
                Id = 3,
                Text = "Opt C",
                QuestionId = 2,
                Points = 3,
            },
            new()
            {
                Id = 1,
                Text = "Yes",
                QuestionId = 4,
                Points = 1
            },
            new()
            {
                Id = 2,
                Text = "No",
                QuestionId = 4,
                Points = 0
            },
            new()
            {
                Id = 1,
                Text = "Of course",
                QuestionId = 5,
                Points = 2
            },
            new()
            {
                Id = 2,
                Text = "Of course not",
                QuestionId = 5,
                Points = -2
            },
            new()
            {
                Id = 3,
                Text = "Jebu ti mamu",
                QuestionId = 5,
                Points = 2
            },
            new()
            {
                Id = 4,
                Text = "Hele cudlik!!!",
                QuestionId = 5,
                Points = 0
            }

        };
        return options;
    }
}

using Domain.Entities.Tests.Answers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;
public class AnswerSimilarityConfiguration : IEntityTypeConfiguration<AnswerSimilarity>
{
    public void Configure(EntityTypeBuilder<AnswerSimilarity> builder)
    {
        builder.HasKey(x => new { x.QuestionId, x.SubmissionId, x.CheckAgainstSubmissionId });
    }
}

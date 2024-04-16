using Domain.Entities.Tests.Submissions.Reviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;
public class TestSubmissionReviewConfiguration : IEntityTypeConfiguration<TestSubmissionReview>
{
    public void Configure(EntityTypeBuilder<TestSubmissionReview> builder)
    {
        builder.HasKey(x => new { x.SubmissionId, x.QuestionId });
    }
}

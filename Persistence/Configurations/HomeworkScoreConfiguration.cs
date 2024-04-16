using Domain.Entities.Homeworks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;
public class HomeworkScoreConfiguration : IEntityTypeConfiguration<HomeworkScore>
{
    public void Configure(EntityTypeBuilder<HomeworkScore> builder)
    {
        builder.HasKey(x => new { x.UserId, x.HomeworkId });
    }
}

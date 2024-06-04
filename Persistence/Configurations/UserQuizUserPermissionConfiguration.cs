using Domain.Entities.UserQuizzes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;
public class UserQuizUserPermissionConfiguration : IEntityTypeConfiguration<UserQuizUserPermission>
{
    public void Configure(EntityTypeBuilder<UserQuizUserPermission> builder)
    {
        builder.HasKey(x => new { x.UserQuizId, x.UserId }); 
    }
}

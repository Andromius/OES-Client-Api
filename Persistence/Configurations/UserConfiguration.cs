using Domain.Entities.Users;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(SeedUsers());
    }

    private static IEnumerable<User> SeedUsers()
    {
        List<User> users = new()
        {
            new User(1, "Admin", "Doe", "admin", PasswordService.GetHash("admin"), UserRole.Admin),
            new User(2, "Teacher", "Doe", "teacher", PasswordService.GetHash("teacher"), UserRole.Teacher),
            new User(3, "Student", "Doe", "student", PasswordService.GetHash("student"), UserRole.Student),
            new User(4, "Alice", "Doe", "teachMaster", PasswordService.GetHash("teacher"), UserRole.Teacher),
            new User(5, "Bob", "Doe", "eduGuru", PasswordService.GetHash("teacher"), UserRole.Teacher),
            new User(6, "Charlie", "Doe", "profLearner", PasswordService.GetHash("teacher"), UserRole.Teacher),
            new User(7, "David", "Doe", "learnSculptor", PasswordService.GetHash("teacher"), UserRole.Teacher),
            new User(8, "Ella", "Doe", "knowledgeNinja", PasswordService.GetHash("teacher"), UserRole.Teacher),
            new User(9, "Frank", "Doe", "scholarSavvy", PasswordService.GetHash("teacher"), UserRole.Teacher),
            new User(10, "Grace", "Doe", "eduMaestro", PasswordService.GetHash("teacher"), UserRole.Teacher),
            new User(11, "Hannah", "Smith", "brainyTutor", PasswordService.GetHash("teacher"), UserRole.Teacher),
            new User(12, "Isaac", "Johnson", "wisdomWhiz", PasswordService.GetHash("teacher"), UserRole.Teacher)
        };

        return users;
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence;

#nullable disable

namespace OESAppApi.Migrations
{
    [DbContext(typeof(OESAppApiDbContext))]
    [Migration("20240129141859_Notes")]
    partial class Notes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Courses.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Color")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Course");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "ABC12",
                            Description = "You will learn basic skill about python",
                            Name = "Python",
                            ShortName = "P"
                        },
                        new
                        {
                            Id = 2,
                            Code = "ABC21",
                            Color = "0xffec6337",
                            Description = "You will learn basic skill about english",
                            Name = "English",
                            ShortName = "E"
                        },
                        new
                        {
                            Id = 3,
                            Code = "CBA12",
                            Color = "0xff00ff00",
                            Description = "You will learn basic skill about java",
                            Name = "Java",
                            ShortName = "J"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Courses.CourseItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CourseItem");

                    b.HasDiscriminator<string>("Discriminator").HasValue("CourseItem");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Domain.Entities.Courses.CourseXUser", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("UserRole")
                        .HasColumnType("integer");

                    b.HasKey("CourseId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("CourseXUser");

                    b.HasData(
                        new
                        {
                            CourseId = 1,
                            UserId = 3,
                            UserRole = 1
                        },
                        new
                        {
                            CourseId = 2,
                            UserId = 3,
                            UserRole = 1
                        },
                        new
                        {
                            CourseId = 3,
                            UserId = 3,
                            UserRole = 1
                        },
                        new
                        {
                            CourseId = 1,
                            UserId = 2,
                            UserRole = 0
                        },
                        new
                        {
                            CourseId = 2,
                            UserId = 2,
                            UserRole = 0
                        },
                        new
                        {
                            CourseId = 3,
                            UserId = 2,
                            UserRole = 0
                        });
                });

            modelBuilder.Entity("Domain.Entities.Devices.DevicePlatform", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("PlatformName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DevicePlatform");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            PlatformName = "Other"
                        },
                        new
                        {
                            Id = 1,
                            PlatformName = "Android"
                        },
                        new
                        {
                            Id = 2,
                            PlatformName = "iOS"
                        },
                        new
                        {
                            Id = 3,
                            PlatformName = "Windows"
                        },
                        new
                        {
                            Id = 4,
                            PlatformName = "MacOS"
                        },
                        new
                        {
                            Id = 5,
                            PlatformName = "Linux"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Sessions.Session", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("DevicePlatformId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsWeb")
                        .HasColumnType("boolean");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Token");

                    b.HasIndex("DevicePlatformId");

                    b.HasIndex("UserId");

                    b.ToTable("Session");
                });

            modelBuilder.Entity("Domain.Entities.Tests.Answers.Answer", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer");

                    b.Property<int>("TestSubmissionId")
                        .HasColumnType("integer");

                    b.Property<int>("QuestionTestId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id", "QuestionId", "TestSubmissionId");

                    b.HasIndex("TestSubmissionId");

                    b.HasIndex("QuestionId", "QuestionTestId");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("Domain.Entities.Tests.Questions.Option", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer");

                    b.Property<int>("QuestionTestId")
                        .HasColumnType("integer");

                    b.Property<int>("Points")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id", "QuestionId", "QuestionTestId");

                    b.HasIndex("QuestionId", "QuestionTestId");

                    b.ToTable("Options");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            QuestionId = 1,
                            QuestionTestId = 1,
                            Points = 3,
                            Text = "Opt A"
                        },
                        new
                        {
                            Id = 2,
                            QuestionId = 1,
                            QuestionTestId = 1,
                            Points = 0,
                            Text = "Opt B"
                        },
                        new
                        {
                            Id = 3,
                            QuestionId = 1,
                            QuestionTestId = 1,
                            Points = 0,
                            Text = "Opt C"
                        },
                        new
                        {
                            Id = 1,
                            QuestionId = 2,
                            QuestionTestId = 1,
                            Points = 3,
                            Text = "Opt A"
                        },
                        new
                        {
                            Id = 2,
                            QuestionId = 2,
                            QuestionTestId = 1,
                            Points = -3,
                            Text = "Opt B"
                        },
                        new
                        {
                            Id = 3,
                            QuestionId = 2,
                            QuestionTestId = 1,
                            Points = 3,
                            Text = "Opt C"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Tests.Questions.Question", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<int>("TestId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Points")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id", "TestId");

                    b.HasIndex("TestId");

                    b.ToTable("Question");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TestId = 1,
                            Description = "What is hello?",
                            Name = "Question 1",
                            Points = 3,
                            Type = "pick-one"
                        },
                        new
                        {
                            Id = 2,
                            TestId = 1,
                            Description = "Pick many question.",
                            Name = "Question 2",
                            Points = 6,
                            Type = "pick-many"
                        },
                        new
                        {
                            Id = 3,
                            TestId = 1,
                            Description = "Write hello.",
                            Name = "Question 3",
                            Points = 10,
                            Type = "open"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Tests.TestSubmission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("GradedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime>("SubmittedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("TestId")
                        .HasColumnType("integer");

                    b.Property<int?>("TotalPoints")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.HasIndex("UserId");

                    b.ToTable("TestSubmission");
                });

            modelBuilder.Entity("Domain.Entities.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Admin",
                            LastName = "Doe",
                            Password = "$2a$11$3kBuqYM3txGMmAVJUGMXwuCbIzWyBgZQ/5XIGwQBtpZKJAJZg5Mou",
                            Role = 0,
                            Username = "admin"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Teacher",
                            LastName = "Doe",
                            Password = "$2a$11$yHSUNzk3NAVN3s5XHSGGZuzs4RXh6nnbKU8DLVg2gtbWlBvsIFXKC",
                            Role = 1,
                            Username = "teacher"
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Student",
                            LastName = "Doe",
                            Password = "$2a$11$edK2/IF.xzIA/z8/TjvRJ.gac39Ma4Ie52JkPGc7uYNnzwLqzJaI2",
                            Role = 2,
                            Username = "student"
                        },
                        new
                        {
                            Id = 4,
                            FirstName = "Alice",
                            LastName = "Doe",
                            Password = "$2a$11$.QzRELeVg39tAkJceup5fu/T3lv/oQTcZaMhKC77oryLI4860FZyO",
                            Role = 1,
                            Username = "teachMaster"
                        },
                        new
                        {
                            Id = 5,
                            FirstName = "Bob",
                            LastName = "Doe",
                            Password = "$2a$11$VX.66lQMROHE02mKTlHjSurAl00ZN.ZrMpjqswXQLABnooxwSGyjS",
                            Role = 1,
                            Username = "eduGuru"
                        },
                        new
                        {
                            Id = 6,
                            FirstName = "Charlie",
                            LastName = "Doe",
                            Password = "$2a$11$3yGg0VMkhboq5ZkjPKPgl.8SkZVguDkd2uGI/kf0u5mRK9wr6orhS",
                            Role = 1,
                            Username = "profLearner"
                        },
                        new
                        {
                            Id = 7,
                            FirstName = "David",
                            LastName = "Doe",
                            Password = "$2a$11$4Y9wf1hplDb/IznK6uZGqO5T86ZYNrDR17HLzqCNDCIXaFCPgD5g6",
                            Role = 1,
                            Username = "learnSculptor"
                        },
                        new
                        {
                            Id = 8,
                            FirstName = "Ella",
                            LastName = "Doe",
                            Password = "$2a$11$V3//JercN529msTKF1VJL.CfnNx1WepexmN9RiAqDA4d3lV.9h6fO",
                            Role = 1,
                            Username = "knowledgeNinja"
                        },
                        new
                        {
                            Id = 9,
                            FirstName = "Frank",
                            LastName = "Doe",
                            Password = "$2a$11$pWqLofFYPWXiA0VNuTribOSX9PdvY8Wba7P7f4bi/zLFA9.lWIfai",
                            Role = 1,
                            Username = "scholarSavvy"
                        },
                        new
                        {
                            Id = 10,
                            FirstName = "Grace",
                            LastName = "Doe",
                            Password = "$2a$11$UIhEYJTu87fpC9rnX.PAcOY/OPdhbPqrZ81XwMGYxvK3k57XAIAIO",
                            Role = 1,
                            Username = "eduMaestro"
                        },
                        new
                        {
                            Id = 11,
                            FirstName = "Hannah",
                            LastName = "Smith",
                            Password = "$2a$11$NETNrKtirlF7cNk3nmYItePEWF.1/IxmvmKpCTcHJZclz8l7O0mAG",
                            Role = 1,
                            Username = "brainyTutor"
                        },
                        new
                        {
                            Id = 12,
                            FirstName = "Isaac",
                            LastName = "Johnson",
                            Password = "$2a$11$oMM16DItBt.RxdicYl59wOBVZRYFfokBrMYtOpJT69HJ4EprFWTU.",
                            Role = 1,
                            Username = "wisdomWhiz"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Notes.Note", b =>
                {
                    b.HasBaseType("Domain.Entities.Courses.CourseItem");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasIndex("CourseId");

                    b.HasDiscriminator().HasValue("Note");
                });

            modelBuilder.Entity("Domain.Entities.Tests.Test", b =>
                {
                    b.HasBaseType("Domain.Entities.Courses.CourseItem");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<DateTime>("End")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MaxAttempts")
                        .HasColumnType("integer");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Scheduled")
                        .HasColumnType("timestamp with time zone");

                    b.HasIndex("CourseId");

                    b.HasDiscriminator().HasValue("Test");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CourseId = 3,
                            Created = new DateTime(2024, 1, 29, 14, 18, 59, 804, DateTimeKind.Utc).AddTicks(2311),
                            IsVisible = true,
                            Name = "Write 100x hello!",
                            UserId = 1,
                            Duration = 1800,
                            End = new DateTime(2024, 1, 29, 17, 18, 59, 804, DateTimeKind.Utc).AddTicks(2316),
                            MaxAttempts = 3,
                            Password = "password",
                            Scheduled = new DateTime(2024, 1, 29, 14, 48, 59, 804, DateTimeKind.Utc).AddTicks(2312)
                        });
                });

            modelBuilder.Entity("Domain.Entities.Courses.CourseItem", b =>
                {
                    b.HasOne("Domain.Entities.Users.User", "User")
                        .WithMany("CourseItems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Courses.CourseXUser", b =>
                {
                    b.HasOne("Domain.Entities.Courses.Course", "Course")
                        .WithMany("Users")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Users.User", "User")
                        .WithMany("Courses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Sessions.Session", b =>
                {
                    b.HasOne("Domain.Entities.Devices.DevicePlatform", "DevicePlatform")
                        .WithMany()
                        .HasForeignKey("DevicePlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Users.User", "User")
                        .WithMany("Sessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DevicePlatform");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Tests.Answers.Answer", b =>
                {
                    b.HasOne("Domain.Entities.Tests.TestSubmission", "TestSubmission")
                        .WithMany("Answers")
                        .HasForeignKey("TestSubmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Tests.Questions.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId", "QuestionTestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("TestSubmission");
                });

            modelBuilder.Entity("Domain.Entities.Tests.Questions.Option", b =>
                {
                    b.HasOne("Domain.Entities.Tests.Questions.Question", "Question")
                        .WithMany("Options")
                        .HasForeignKey("QuestionId", "QuestionTestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Domain.Entities.Tests.Questions.Question", b =>
                {
                    b.HasOne("Domain.Entities.Tests.Test", "Test")
                        .WithMany("Questions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");
                });

            modelBuilder.Entity("Domain.Entities.Tests.TestSubmission", b =>
                {
                    b.HasOne("Domain.Entities.Tests.Test", "Test")
                        .WithMany("Submissions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Notes.Note", b =>
                {
                    b.HasOne("Domain.Entities.Courses.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Domain.Entities.Tests.Test", b =>
                {
                    b.HasOne("Domain.Entities.Courses.Course", "Course")
                        .WithMany("Tests")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Domain.Entities.Courses.Course", b =>
                {
                    b.Navigation("Tests");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Domain.Entities.Tests.Questions.Question", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("Domain.Entities.Tests.TestSubmission", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("Domain.Entities.Users.User", b =>
                {
                    b.Navigation("CourseItems");

                    b.Navigation("Courses");

                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("Domain.Entities.Tests.Test", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("Submissions");
                });
#pragma warning restore 612, 618
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OESAppApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ShortName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DevicePlatform",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    PlatformName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevicePlatform", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourseItemType = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    MaxAttempts = table.Column<int>(type: "integer", nullable: true),
                    Scheduled = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Task = table.Column<string>(type: "text", nullable: true),
                    Data = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseItem_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseItem_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseXUser",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    UserRole = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseXUser", x => new { x.CourseId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CourseXUser_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseXUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    Token = table.Column<string>(type: "text", nullable: false),
                    DeviceName = table.Column<string>(type: "text", nullable: false),
                    IsWeb = table.Column<bool>(type: "boolean", nullable: false),
                    DevicePlatformId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.Token);
                    table.ForeignKey(
                        name: "FK_Session_DevicePlatform_DevicePlatformId",
                        column: x => x.DevicePlatformId,
                        principalTable: "DevicePlatform",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Session_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomeworkScore",
                columns: table => new
                {
                    HomeworkId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Points = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeworkScore", x => new { x.UserId, x.HomeworkId });
                    table.ForeignKey(
                        name: "FK_HomeworkScore_CourseItem_HomeworkId",
                        column: x => x.HomeworkId,
                        principalTable: "CourseItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeworkScore_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomeworkSubmission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    HomeworkId = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true),
                    Comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeworkSubmission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeworkSubmission_CourseItem_HomeworkId",
                        column: x => x.HomeworkId,
                        principalTable: "CourseItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeworkSubmission_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Points = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    EvaluatableId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_CourseItem_EvaluatableId",
                        column: x => x.EvaluatableId,
                        principalTable: "CourseItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestSubmission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    TestId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GradedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TotalPoints = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSubmission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestSubmission_CourseItem_TestId",
                        column: x => x.TestId,
                        principalTable: "CourseItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestSubmission_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomeworkSubmissionAttachment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubmissionId = table.Column<int>(type: "integer", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    File = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeworkSubmissionAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeworkSubmissionAttachment_HomeworkSubmission_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "HomeworkSubmission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Points = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => new { x.Id, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_Options_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    TestSubmissionId = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => new { x.Id, x.QuestionId, x.TestSubmissionId });
                    table.ForeignKey(
                        name: "FK_Answer_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answer_TestSubmission_TestSubmissionId",
                        column: x => x.TestSubmissionId,
                        principalTable: "TestSubmission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestSubmissionReview",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    SubmissionId = table.Column<int>(type: "integer", nullable: false),
                    Points = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSubmissionReview", x => new { x.SubmissionId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_TestSubmissionReview_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestSubmissionReview_TestSubmission_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "TestSubmission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "Id", "Code", "Color", "Description", "Name", "ShortName" },
                values: new object[,]
                {
                    { 1, "ABC12", null, "You will learn basic skill about python", "Python", "P" },
                    { 2, "ABC21", "0xffec6337", "You will learn basic skill about english", "English", "E" },
                    { 3, "CBA12", "0xff00ff00", "You will learn basic skill about java", "Java", "J" }
                });

            migrationBuilder.InsertData(
                table: "DevicePlatform",
                columns: new[] { "Id", "PlatformName" },
                values: new object[,]
                {
                    { 0, "Other" },
                    { 1, "Android" },
                    { 2, "iOS" },
                    { 3, "Windows" },
                    { 4, "MacOS" },
                    { 5, "Linux" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "Admin", "Doe", "$2a$11$Pw75BcZ2.vwyE1rkgw6/2u0qKHSHkOzLYk0BFbPbSuhpDa6TCJNU.", 0, "admin" },
                    { 2, "Teacher", "Doe", "$2a$11$51r8P6YNu1rjkCqvNvs9yulhZEmZGNTzY7f.F5jw631Anr8r0cM4i", 1, "teacher" },
                    { 3, "Student", "Doe", "$2a$11$7Ld/DnIa.rJlTwDwp1vqIusPNP5WROALun3xtyK3ulPBAypogjpEK", 2, "student" },
                    { 4, "Alice", "Doe", "$2a$11$vsun2IajKE19cmSKvIQ1yueKHBFdS5RtVFsfKty8DUNzWwC0.3wuS", 1, "teachMaster" },
                    { 5, "Bob", "Doe", "$2a$11$Qsf2xpV9n3l18JB.muE7WummMmGwyOM/OwHxu9NrFam8bwhlAXlqy", 1, "eduGuru" },
                    { 6, "Charlie", "Doe", "$2a$11$yNeoQc7rYTFreIg7Ycw/ruELLpAmJe4huqudG5wON2mwbpBDL2emW", 1, "profLearner" },
                    { 7, "David", "Doe", "$2a$11$EYS6tyXedvDBum0n/j4qBehpm.qiSWvjAs/EWEwI9XsWnQH132EFG", 1, "learnSculptor" },
                    { 8, "Ella", "Doe", "$2a$11$fQ4673Ub0RxUqjzqMieFke1c4.dINmKSQjZGqlk7N6fat4Kg0Kt3m", 1, "knowledgeNinja" },
                    { 9, "Frank", "Doe", "$2a$11$DlnOMEdCDvAQjnpobxxVqe53aT.VB0Zbq6KzY1dhZNWtyX3PcVzTi", 1, "scholarSavvy" },
                    { 10, "Grace", "Doe", "$2a$11$UmY21pGi1FZ3FPqUU3Dn8eksFVYRqK4g0f1in1CLyrbe3LRGJVL4a", 1, "eduMaestro" },
                    { 11, "Hannah", "Smith", "$2a$11$6BybJa/4NrVHZuBu7utbT.4hO0edhpF.ub/OYtkvwgbhvUoF8FVsa", 1, "brainyTutor" },
                    { 12, "Isaac", "Johnson", "$2a$11$Jqg/HcwUy1BRk05z6xBIreuWRBz7P04zHr7uo9/ApFn9AN37Ea.fK", 1, "wisdomWhiz" }
                });

            migrationBuilder.InsertData(
                table: "CourseItem",
                columns: new[] { "Id", "CourseId", "CourseItemType", "Created", "Duration", "End", "IsVisible", "MaxAttempts", "Name", "Password", "Scheduled", "UserId" },
                values: new object[] { 1, 3, 0, new DateTime(2024, 4, 13, 19, 10, 3, 178, DateTimeKind.Utc).AddTicks(7605), 1800, new DateTime(2024, 4, 13, 22, 10, 3, 178, DateTimeKind.Utc).AddTicks(7613), true, 3, "Write 100x hello!", "password", new DateTime(2024, 4, 13, 19, 40, 3, 178, DateTimeKind.Utc).AddTicks(7607), 1 });

            migrationBuilder.InsertData(
                table: "CourseItem",
                columns: new[] { "Id", "CourseId", "CourseItemType", "Created", "End", "IsVisible", "Name", "Scheduled", "UserId" },
                values: new object[] { 2, 3, 4, new DateTime(2024, 4, 13, 19, 10, 3, 178, DateTimeKind.Utc).AddTicks(986), new DateTime(2024, 4, 13, 22, 10, 3, 178, DateTimeKind.Utc).AddTicks(991), true, "AMAZING QUIZ", new DateTime(2024, 4, 13, 19, 40, 3, 178, DateTimeKind.Utc).AddTicks(989), 1 });

            migrationBuilder.InsertData(
                table: "CourseXUser",
                columns: new[] { "CourseId", "UserId", "UserRole" },
                values: new object[,]
                {
                    { 1, 2, 0 },
                    { 1, 3, 1 },
                    { 2, 2, 0 },
                    { 2, 3, 1 },
                    { 3, 2, 0 },
                    { 3, 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "Question",
                columns: new[] { "Id", "Description", "EvaluatableId", "Name", "Points", "Type" },
                values: new object[,]
                {
                    { 1, "What is hello?", 1, "Question 1", 3, "pick-one" },
                    { 2, "Pick many question.", 1, "Question 2", 6, "pick-many" },
                    { 3, "Write hello.", 1, "Question 3", 10, "open" },
                    { 4, "Write hello.", 2, "Question YESNO", 1, "pick-one" },
                    { 5, "Kaja je dobry programator", 2, "Question YESNO", 4, "pick-many" }
                });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "QuestionId", "Points", "Text" },
                values: new object[,]
                {
                    { 1, 1, 3, "Opt A" },
                    { 1, 2, 3, "Opt A" },
                    { 1, 4, 1, "Yes" },
                    { 1, 5, 2, "Of course" },
                    { 2, 1, 0, "Opt B" },
                    { 2, 2, -3, "Opt B" },
                    { 2, 4, 0, "No" },
                    { 2, 5, -2, "Of course not" },
                    { 3, 1, 0, "Opt C" },
                    { 3, 2, 3, "Opt C" },
                    { 3, 5, 2, "Jebu ti mamu" },
                    { 4, 5, 0, "Hele cudlik!!!" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_TestSubmissionId",
                table: "Answer",
                column: "TestSubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseItem_CourseId",
                table: "CourseItem",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseItem_UserId",
                table: "CourseItem",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseXUser_UserId",
                table: "CourseXUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkScore_HomeworkId",
                table: "HomeworkScore",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkSubmission_HomeworkId",
                table: "HomeworkSubmission",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkSubmission_UserId",
                table: "HomeworkSubmission",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkSubmissionAttachment_SubmissionId",
                table: "HomeworkSubmissionAttachment",
                column: "SubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_QuestionId",
                table: "Options",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_EvaluatableId",
                table: "Question",
                column: "EvaluatableId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_DevicePlatformId",
                table: "Session",
                column: "DevicePlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_UserId",
                table: "Session",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TestSubmission_TestId",
                table: "TestSubmission",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestSubmission_UserId",
                table: "TestSubmission",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TestSubmissionReview_QuestionId",
                table: "TestSubmissionReview",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "CourseXUser");

            migrationBuilder.DropTable(
                name: "HomeworkScore");

            migrationBuilder.DropTable(
                name: "HomeworkSubmissionAttachment");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "TestSubmissionReview");

            migrationBuilder.DropTable(
                name: "HomeworkSubmission");

            migrationBuilder.DropTable(
                name: "DevicePlatform");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "TestSubmission");

            migrationBuilder.DropTable(
                name: "CourseItem");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

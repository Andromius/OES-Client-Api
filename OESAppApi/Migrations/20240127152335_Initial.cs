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
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    Scheduled = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Duration = table.Column<int>(type: "integer", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    MaxAttempts = table.Column<int>(type: "integer", nullable: true)
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
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    TestId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Points = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => new { x.Id, x.TestId });
                    table.ForeignKey(
                        name: "FK_Question_CourseItem_TestId",
                        column: x => x.TestId,
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
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    QuestionTestId = table.Column<int>(type: "integer", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Points = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => new { x.Id, x.QuestionId, x.QuestionTestId });
                    table.ForeignKey(
                        name: "FK_Options_Question_QuestionId_QuestionTestId",
                        columns: x => new { x.QuestionId, x.QuestionTestId },
                        principalTable: "Question",
                        principalColumns: new[] { "Id", "TestId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    TestSubmissionId = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    QuestionTestId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => new { x.Id, x.QuestionId, x.TestSubmissionId });
                    table.ForeignKey(
                        name: "FK_Answer_Question_QuestionId_QuestionTestId",
                        columns: x => new { x.QuestionId, x.QuestionTestId },
                        principalTable: "Question",
                        principalColumns: new[] { "Id", "TestId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answer_TestSubmission_TestSubmissionId",
                        column: x => x.TestSubmissionId,
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
                    { 1, "Admin", "Doe", "$2a$11$zze7INAi/ENBWfpwGrIN8OvbmJqZqdCfuuuZAIIAaDV/7XGaTQ4bS", 0, "admin" },
                    { 2, "Teacher", "Doe", "$2a$11$CvjgvrV2x8vX1rSZhyrPb.0hzX2F/qSL4EU1dKtFj9yeaGqd7GVzm", 1, "teacher" },
                    { 3, "Student", "Doe", "$2a$11$mN69wbQYRIherroQGUyTH.ziAddN2nsgHv74JrkBctBgxA2Qv4GYu", 2, "student" },
                    { 4, "Alice", "Doe", "$2a$11$1uI8eeeAvgRapnsv9hgFZOTOHer7TLYEU1hq4b1NtmuTqmMWDapI2", 1, "teachMaster" },
                    { 5, "Bob", "Doe", "$2a$11$KFV3EqNAQeMZgaTOYwVWQeWJV1M2ApRKdBa6jNp1iRPKt6Up7d0wK", 1, "eduGuru" },
                    { 6, "Charlie", "Doe", "$2a$11$eDjE78HN.T6brV9jztHCq.U/nqPhGpc/gjNj48wX.KTyXrRN3YHau", 1, "profLearner" },
                    { 7, "David", "Doe", "$2a$11$OTMHUZOh.UQ6mvw70/ukWu5Unmr.Ebr8I17NaK8i586ii6tjcoMri", 1, "learnSculptor" },
                    { 8, "Ella", "Doe", "$2a$11$/HwIyifXjy3pUDi8o4dxEu4nwC/tJ0JPiOpQtUnLFERIOqYd9x73S", 1, "knowledgeNinja" },
                    { 9, "Frank", "Doe", "$2a$11$AUmZ.vbILKVH5a9jKQA0QOjOe3ohJ5jnosw/y1MpBww3RQa5Gzvce", 1, "scholarSavvy" },
                    { 10, "Grace", "Doe", "$2a$11$RYEZMGLZQ/OPpMPca3uPQe55QZNHQVoFXu.lFhW76MrX0b0dViuH2", 1, "eduMaestro" },
                    { 11, "Hannah", "Smith", "$2a$11$doQqTv6Sass2aqBgt.GQk.PMh8vZTxygzgdP4tGhh2Y/6sGQwGGjq", 1, "brainyTutor" },
                    { 12, "Isaac", "Johnson", "$2a$11$DyzTNT3s13DtDV9Ij9zgy.sAG0nXFMIQeLA2wwDlw/nGInmL3JZHC", 1, "wisdomWhiz" }
                });

            migrationBuilder.InsertData(
                table: "CourseItem",
                columns: new[] { "Id", "CourseId", "Created", "Discriminator", "Duration", "End", "IsVisible", "MaxAttempts", "Name", "Password", "Scheduled", "UserId" },
                values: new object[] { 1, 3, new DateTime(2024, 1, 27, 15, 23, 35, 720, DateTimeKind.Utc).AddTicks(9836), "Test", 1800, new DateTime(2024, 1, 27, 18, 23, 35, 720, DateTimeKind.Utc).AddTicks(9842), true, 3, "Write 100x hello!", "password", new DateTime(2024, 1, 27, 15, 53, 35, 720, DateTimeKind.Utc).AddTicks(9837), 1 });

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
                columns: new[] { "Id", "TestId", "Description", "Name", "Points", "Type" },
                values: new object[,]
                {
                    { 1, 1, "What is hello?", "Question 1", 3, "pick-one" },
                    { 2, 1, "Pick many question.", "Question 2", 6, "pick-many" },
                    { 3, 1, "Write hello.", "Question 3", 10, "open" }
                });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "QuestionId", "QuestionTestId", "Points", "Text" },
                values: new object[,]
                {
                    { 1, 1, 1, 3, "Opt A" },
                    { 1, 2, 1, 3, "Opt A" },
                    { 2, 1, 1, 0, "Opt B" },
                    { 2, 2, 1, -3, "Opt B" },
                    { 3, 1, 1, 0, "Opt C" },
                    { 3, 2, 1, 3, "Opt C" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId_QuestionTestId",
                table: "Answer",
                columns: new[] { "QuestionId", "QuestionTestId" });

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
                name: "IX_Options_QuestionId_QuestionTestId",
                table: "Options",
                columns: new[] { "QuestionId", "QuestionTestId" });

            migrationBuilder.CreateIndex(
                name: "IX_Question_TestId",
                table: "Question",
                column: "TestId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "CourseXUser");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "TestSubmission");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "DevicePlatform");

            migrationBuilder.DropTable(
                name: "CourseItem");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

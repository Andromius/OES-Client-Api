using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OESAppApi.Migrations
{
    /// <inheritdoc />
    public partial class AnswerSimilarityChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerSimilarity_User_ChallengerId",
                table: "AnswerSimilarity");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerSimilarity_User_SubmittorId",
                table: "AnswerSimilarity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnswerSimilarity",
                table: "AnswerSimilarity");

            migrationBuilder.DropIndex(
                name: "IX_AnswerSimilarity_SubmittorId",
                table: "AnswerSimilarity");

            migrationBuilder.DropColumn(
                name: "SubmittorId",
                table: "AnswerSimilarity");

            migrationBuilder.RenameColumn(
                name: "ChallengerId",
                table: "AnswerSimilarity",
                newName: "CheckAgainstSubmissionId");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerSimilarity_ChallengerId",
                table: "AnswerSimilarity",
                newName: "IX_AnswerSimilarity_CheckAgainstSubmissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnswerSimilarity",
                table: "AnswerSimilarity",
                columns: new[] { "QuestionId", "SubmissionId", "CheckAgainstSubmissionId" });

            migrationBuilder.UpdateData(
                table: "CourseItem",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "End", "Scheduled" },
                values: new object[] { new DateTime(2024, 4, 17, 12, 46, 9, 340, DateTimeKind.Utc).AddTicks(4369), new DateTime(2024, 4, 17, 15, 46, 9, 340, DateTimeKind.Utc).AddTicks(4376), new DateTime(2024, 4, 17, 13, 16, 9, 340, DateTimeKind.Utc).AddTicks(4371) });

            migrationBuilder.UpdateData(
                table: "CourseItem",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "End", "Scheduled" },
                values: new object[] { new DateTime(2024, 4, 17, 12, 46, 9, 339, DateTimeKind.Utc).AddTicks(9942), new DateTime(2024, 4, 17, 15, 46, 9, 339, DateTimeKind.Utc).AddTicks(9948), new DateTime(2024, 4, 17, 13, 16, 9, 339, DateTimeKind.Utc).AddTicks(9944) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$u2sv2tovH.mdz6stV0jI6ex5ybxKmQ588cQayAIErpO1Y.urNSMDW");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$EHMA/U2R94WuAkarQB2xW.sN9n98XHQFjVN0eu/KlfmjRzHy6BKxq");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$zQDdkGi8PDSQQqEphTxcs.M.e8zGaAWbw9wZk1bbMcwGvW1MDCWiW");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$yyTShblSs.55UqH7QXCzCubxZcMpolnqgw/E6M2UmVWUak7u.ZM0m");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "$2a$11$T6G7M8G0ZBBN6uakcAb5Ze0pj0LPO2dPv3pDZlCFGveFf/1kO5sbK");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 6,
                column: "Password",
                value: "$2a$11$chO6Kx8BYbJezAwuuQnlEe5v7hJEaGCZcSZI7G9ZmDiSmNJuPr0GS");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 7,
                column: "Password",
                value: "$2a$11$VaQMNWN3k9El8buteRe2COhm30Scv105K63yb4b3RrhJZ.0NulaT2");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 8,
                column: "Password",
                value: "$2a$11$apE6/Z8Mia7Xl1uyGxKfveu.hVVyDpXe8PySQmV.yKSJgEgKpxSuK");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 9,
                column: "Password",
                value: "$2a$11$utr0u9cgxm3ISyjFM/6wP.pY50jyZ0e6E8LLRC5NYD6cVI9ABNEYu");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 10,
                column: "Password",
                value: "$2a$11$vwnueHCjfyMAtTA88PlMMOwCWTeNV9A.AWKMzjauKLbwCbStCArom");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 11,
                column: "Password",
                value: "$2a$11$CAC2HpZM.9MJvjWXRyVcFe3gCPWGLC2RtYk4DFRgRCUZRF.Cb0Hqq");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 12,
                column: "Password",
                value: "$2a$11$wU578E6a5yfZaZ3pnkmxLuWaBDqUqChQwwzvw8F1BKugkjrpX0iGS");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerSimilarity_TestSubmission_CheckAgainstSubmissionId",
                table: "AnswerSimilarity",
                column: "CheckAgainstSubmissionId",
                principalTable: "TestSubmission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerSimilarity_TestSubmission_CheckAgainstSubmissionId",
                table: "AnswerSimilarity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnswerSimilarity",
                table: "AnswerSimilarity");

            migrationBuilder.RenameColumn(
                name: "CheckAgainstSubmissionId",
                table: "AnswerSimilarity",
                newName: "ChallengerId");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerSimilarity_CheckAgainstSubmissionId",
                table: "AnswerSimilarity",
                newName: "IX_AnswerSimilarity_ChallengerId");

            migrationBuilder.AddColumn<int>(
                name: "SubmittorId",
                table: "AnswerSimilarity",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnswerSimilarity",
                table: "AnswerSimilarity",
                columns: new[] { "QuestionId", "SubmittorId", "ChallengerId", "SubmissionId" });

            migrationBuilder.UpdateData(
                table: "CourseItem",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "End", "Scheduled" },
                values: new object[] { new DateTime(2024, 4, 14, 23, 0, 56, 971, DateTimeKind.Utc).AddTicks(4148), new DateTime(2024, 4, 15, 2, 0, 56, 971, DateTimeKind.Utc).AddTicks(4157), new DateTime(2024, 4, 14, 23, 30, 56, 971, DateTimeKind.Utc).AddTicks(4151) });

            migrationBuilder.UpdateData(
                table: "CourseItem",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "End", "Scheduled" },
                values: new object[] { new DateTime(2024, 4, 14, 23, 0, 56, 970, DateTimeKind.Utc).AddTicks(7898), new DateTime(2024, 4, 15, 2, 0, 56, 970, DateTimeKind.Utc).AddTicks(7909), new DateTime(2024, 4, 14, 23, 30, 56, 970, DateTimeKind.Utc).AddTicks(7904) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$FZZSQ5J1o8gP2Zs99/nB9elrQQw/d4gv3BvUA9smUmH/8pVQf6LdS");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$Gnsr5vx9JbE0/w.pk9Nj.eWMZV7K4iQ4/wZ2beqxonCzhaKU2IugS");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$aFHUqATst8eVBk5PDk/GceFfeGvjTrWBv/TYbmSbhFh.ALZOfW2OS");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$unEXhaGMQDe.7tTNcWG/bOfOBs66xPQYg1VG5ssoUfOPOzv6E.idy");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "$2a$11$12MATReeP0yTVQ7KBH9fEeGx3vFkM2AEveedjkabesCp9cB42EMqe");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 6,
                column: "Password",
                value: "$2a$11$pUW8JUKWwiPgkmLzrEGIKe1mS0LCzQqS/LR6kac5wuXICyPxgR8bC");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 7,
                column: "Password",
                value: "$2a$11$33NW6G34Rjz5ssMplGJ1FeqSIJkd7INohxVp4cVqx4EfZQwmtuBHG");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 8,
                column: "Password",
                value: "$2a$11$gLRi31X3/0vetn6.pg5w4OGkwDudg8D/hn0Jl18olYtDUcbLJxKQu");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 9,
                column: "Password",
                value: "$2a$11$6xH476tdnWpLKaUfmO74d.jlvXaijkk4cvR5lA/xEwvD1aTQB0tm.");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 10,
                column: "Password",
                value: "$2a$11$RYI91ofW2s.utD8SE/q75O/XnueHgNM3AL4lI.gMZ7PB2XKEJ5HgO");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 11,
                column: "Password",
                value: "$2a$11$sfNqPRKYtKrYVD7pBn4lMemE0epZTbsLVtb5hEpTIIvIBbdU/E52e");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 12,
                column: "Password",
                value: "$2a$11$fq28j3rO7zMNmJ5DiDmBieRTg4Ks782Im6SrnjrkNkFm5CvTFz5cq");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerSimilarity_SubmittorId",
                table: "AnswerSimilarity",
                column: "SubmittorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerSimilarity_User_ChallengerId",
                table: "AnswerSimilarity",
                column: "ChallengerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerSimilarity_User_SubmittorId",
                table: "AnswerSimilarity",
                column: "SubmittorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

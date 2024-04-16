using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OESAppApi.Migrations
{
    /// <inheritdoc />
    public partial class AnswerSimilarity_Amend : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AnswerSimilarity",
                table: "AnswerSimilarity");

            migrationBuilder.AddColumn<int>(
                name: "SubmissionId",
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
                name: "IX_AnswerSimilarity_SubmissionId",
                table: "AnswerSimilarity",
                column: "SubmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerSimilarity_TestSubmission_SubmissionId",
                table: "AnswerSimilarity",
                column: "SubmissionId",
                principalTable: "TestSubmission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerSimilarity_TestSubmission_SubmissionId",
                table: "AnswerSimilarity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnswerSimilarity",
                table: "AnswerSimilarity");

            migrationBuilder.DropIndex(
                name: "IX_AnswerSimilarity_SubmissionId",
                table: "AnswerSimilarity");

            migrationBuilder.DropColumn(
                name: "SubmissionId",
                table: "AnswerSimilarity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnswerSimilarity",
                table: "AnswerSimilarity",
                columns: new[] { "QuestionId", "SubmittorId", "ChallengerId" });

            migrationBuilder.UpdateData(
                table: "CourseItem",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "End", "Scheduled" },
                values: new object[] { new DateTime(2024, 4, 14, 22, 20, 50, 257, DateTimeKind.Utc).AddTicks(5598), new DateTime(2024, 4, 15, 1, 20, 50, 257, DateTimeKind.Utc).AddTicks(5603), new DateTime(2024, 4, 14, 22, 50, 50, 257, DateTimeKind.Utc).AddTicks(5600) });

            migrationBuilder.UpdateData(
                table: "CourseItem",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "End", "Scheduled" },
                values: new object[] { new DateTime(2024, 4, 14, 22, 20, 50, 257, DateTimeKind.Utc).AddTicks(1256), new DateTime(2024, 4, 15, 1, 20, 50, 257, DateTimeKind.Utc).AddTicks(1262), new DateTime(2024, 4, 14, 22, 50, 50, 257, DateTimeKind.Utc).AddTicks(1259) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$n5Vm/vCmenq4FlyG9IjI4eqolaoHGIDJNOYwYnC4apmoLsB5wVECe");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$Xh5WRCwq/aydLoIgyiIIxum2KfXrdOGFgs2snxFREWvwOC8LoME3u");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$2B.XwsRNweWFC3PogtYxWOrhQY0koc75Leyc9/s6eoavz3RKfijRO");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$plhfkq8OcnAA64cwd.Hge.dwScQkcpuctbhZH0g.BmEJn3.pNFdhC");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "$2a$11$Xfm0.s9UBDOVWdsv3w.G2..76fYW.Z4pXUyI/xbHvbRf5uFkPQQEG");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 6,
                column: "Password",
                value: "$2a$11$VzDnW6dFNb8VKBeUFtB2zuchJzRPyP42UkdszYOWUm6zAal23LS8S");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 7,
                column: "Password",
                value: "$2a$11$xnokCOvy6E/ax5dUWE84YuEEJdB9xAOwaBMr3HGABhUGQ6KH42h7C");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 8,
                column: "Password",
                value: "$2a$11$1u.lovdbNHYNAl5Wn9yQZe.Zq5ChfPJNYnhzluJ/Daha.ZBAPry1C");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 9,
                column: "Password",
                value: "$2a$11$W72ydMpjTN9YLR6d8mycq.Sswq/giDzh5phX1de/gVXCwVdzSs98.");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 10,
                column: "Password",
                value: "$2a$11$0NVawKICboEilLyU1eV7bedID2PWLeBsKUZKwPUJleRcZjMzQzx2m");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 11,
                column: "Password",
                value: "$2a$11$MKIjr.LOQW4F83ItoOLO0OIfPCrHeaNtq.WifiTUL7Fhb5JUBgZl6");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 12,
                column: "Password",
                value: "$2a$11$fBQfQiaRa87MtDACXVtngOJ2xM.G7yweuDK3nDheGp4eebGOt9AlC");
        }
    }
}

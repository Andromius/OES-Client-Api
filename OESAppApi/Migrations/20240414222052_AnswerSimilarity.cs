using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OESAppApi.Migrations
{
    /// <inheritdoc />
    public partial class AnswerSimilarity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnswerSimilarity",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    SubmittorId = table.Column<int>(type: "integer", nullable: false),
                    ChallengerId = table.Column<int>(type: "integer", nullable: false),
                    Similarity = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerSimilarity", x => new { x.QuestionId, x.SubmittorId, x.ChallengerId });
                    table.ForeignKey(
                        name: "FK_AnswerSimilarity_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerSimilarity_User_ChallengerId",
                        column: x => x.ChallengerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerSimilarity_User_SubmittorId",
                        column: x => x.SubmittorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_AnswerSimilarity_ChallengerId",
                table: "AnswerSimilarity",
                column: "ChallengerId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerSimilarity_SubmittorId",
                table: "AnswerSimilarity",
                column: "SubmittorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerSimilarity");

            migrationBuilder.UpdateData(
                table: "CourseItem",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "End", "Scheduled" },
                values: new object[] { new DateTime(2024, 4, 13, 21, 38, 11, 51, DateTimeKind.Utc).AddTicks(4653), new DateTime(2024, 4, 14, 0, 38, 11, 51, DateTimeKind.Utc).AddTicks(4659), new DateTime(2024, 4, 13, 22, 8, 11, 51, DateTimeKind.Utc).AddTicks(4656) });

            migrationBuilder.UpdateData(
                table: "CourseItem",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "End", "Scheduled" },
                values: new object[] { new DateTime(2024, 4, 13, 21, 38, 11, 50, DateTimeKind.Utc).AddTicks(8164), new DateTime(2024, 4, 14, 0, 38, 11, 50, DateTimeKind.Utc).AddTicks(8171), new DateTime(2024, 4, 13, 22, 8, 11, 50, DateTimeKind.Utc).AddTicks(8166) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$BmPm0ffx.xxCt75Dn7hXQuwDiEd4pXifDHvKpYLSrkwDw2h5jfcfq");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$jVubjaLnEecLM2M40pr0WeC8s3eh/v28Z2m0AoBdewRs/xSCCt.dW");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$0T1rmjEF.S2kmp5xNPorPuDMURd/GLzkE6vhSCshIV.DGEcXt5kku");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$58gbnZaUdCv.Doksb4bRsObYnloJe.OLgfe/rRHAd9MKI382sc3qq");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "$2a$11$2H9O8TBjTirpcLWoxI7myeU3JZwCoPVAWfuK3x.dw26ii9GVy/69W");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 6,
                column: "Password",
                value: "$2a$11$YV2frc1LSUIgSbfQOBIZ6OrfAu4pxwWSmkLi3YBDXaZ56.Z/cNsCS");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 7,
                column: "Password",
                value: "$2a$11$aoT4JcxUCWYGDybteYl4L.g9Ydy8ps45zOtVAep1EAC6vw5Wf35mu");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 8,
                column: "Password",
                value: "$2a$11$iYC5TWR0uhc1jRi4KpTgO.p4h0fuNreq5zm5oIe7dRWyez4zdlRdC");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 9,
                column: "Password",
                value: "$2a$11$pUyCkeUGABY9LcMG4BMj5efchfUUPcjXDtILr6/ALYNLpMAWA3ohy");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 10,
                column: "Password",
                value: "$2a$11$6oewpeLdj9L3C1dHyUeMKe9xYaLohoAHe.LKeLfGjwwI3XvtEVXnm");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 11,
                column: "Password",
                value: "$2a$11$1NwXMteBtKCC6Q8.2IrQTubGaKzeJuejKYqiv39WOmOCUH0LNWag2");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 12,
                column: "Password",
                value: "$2a$11$kW8M4Ef49bE0SsQgt6A5kO2Wkkb6famYCd0Q51wiVgnjy6tTI7eOO");
        }
    }
}

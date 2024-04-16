using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OESAppApi.Migrations
{
    /// <inheritdoc />
    public partial class UserQuiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_CourseItem_EvaluatableId",
                table: "Question");

            migrationBuilder.RenameColumn(
                name: "EvaluatableId",
                table: "Question",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Question_EvaluatableId",
                table: "Question",
                newName: "IX_Question_ItemId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Question_CourseItem_ItemId",
                table: "Question",
                column: "ItemId",
                principalTable: "CourseItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_CourseItem_ItemId",
                table: "Question");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Question",
                newName: "EvaluatableId");

            migrationBuilder.RenameIndex(
                name: "IX_Question_ItemId",
                table: "Question",
                newName: "IX_Question_EvaluatableId");

            migrationBuilder.UpdateData(
                table: "CourseItem",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "End", "Scheduled" },
                values: new object[] { new DateTime(2024, 4, 13, 19, 10, 3, 178, DateTimeKind.Utc).AddTicks(7605), new DateTime(2024, 4, 13, 22, 10, 3, 178, DateTimeKind.Utc).AddTicks(7613), new DateTime(2024, 4, 13, 19, 40, 3, 178, DateTimeKind.Utc).AddTicks(7607) });

            migrationBuilder.UpdateData(
                table: "CourseItem",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "End", "Scheduled" },
                values: new object[] { new DateTime(2024, 4, 13, 19, 10, 3, 178, DateTimeKind.Utc).AddTicks(986), new DateTime(2024, 4, 13, 22, 10, 3, 178, DateTimeKind.Utc).AddTicks(991), new DateTime(2024, 4, 13, 19, 40, 3, 178, DateTimeKind.Utc).AddTicks(989) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$Pw75BcZ2.vwyE1rkgw6/2u0qKHSHkOzLYk0BFbPbSuhpDa6TCJNU.");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$51r8P6YNu1rjkCqvNvs9yulhZEmZGNTzY7f.F5jw631Anr8r0cM4i");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$7Ld/DnIa.rJlTwDwp1vqIusPNP5WROALun3xtyK3ulPBAypogjpEK");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$vsun2IajKE19cmSKvIQ1yueKHBFdS5RtVFsfKty8DUNzWwC0.3wuS");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "$2a$11$Qsf2xpV9n3l18JB.muE7WummMmGwyOM/OwHxu9NrFam8bwhlAXlqy");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 6,
                column: "Password",
                value: "$2a$11$yNeoQc7rYTFreIg7Ycw/ruELLpAmJe4huqudG5wON2mwbpBDL2emW");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 7,
                column: "Password",
                value: "$2a$11$EYS6tyXedvDBum0n/j4qBehpm.qiSWvjAs/EWEwI9XsWnQH132EFG");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 8,
                column: "Password",
                value: "$2a$11$fQ4673Ub0RxUqjzqMieFke1c4.dINmKSQjZGqlk7N6fat4Kg0Kt3m");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 9,
                column: "Password",
                value: "$2a$11$DlnOMEdCDvAQjnpobxxVqe53aT.VB0Zbq6KzY1dhZNWtyX3PcVzTi");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 10,
                column: "Password",
                value: "$2a$11$UmY21pGi1FZ3FPqUU3Dn8eksFVYRqK4g0f1in1CLyrbe3LRGJVL4a");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 11,
                column: "Password",
                value: "$2a$11$6BybJa/4NrVHZuBu7utbT.4hO0edhpF.ub/OYtkvwgbhvUoF8FVsa");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 12,
                column: "Password",
                value: "$2a$11$Jqg/HcwUy1BRk05z6xBIreuWRBz7P04zHr7uo9/ApFn9AN37Ea.fK");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_CourseItem_EvaluatableId",
                table: "Question",
                column: "EvaluatableId",
                principalTable: "CourseItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

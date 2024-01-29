using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OESAppApi.Migrations
{
    /// <inheritdoc />
    public partial class Notes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "CourseItem",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CourseItem",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "End", "Scheduled" },
                values: new object[] { new DateTime(2024, 1, 29, 14, 18, 59, 804, DateTimeKind.Utc).AddTicks(2311), new DateTime(2024, 1, 29, 17, 18, 59, 804, DateTimeKind.Utc).AddTicks(2316), new DateTime(2024, 1, 29, 14, 48, 59, 804, DateTimeKind.Utc).AddTicks(2312) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$3kBuqYM3txGMmAVJUGMXwuCbIzWyBgZQ/5XIGwQBtpZKJAJZg5Mou");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$yHSUNzk3NAVN3s5XHSGGZuzs4RXh6nnbKU8DLVg2gtbWlBvsIFXKC");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$edK2/IF.xzIA/z8/TjvRJ.gac39Ma4Ie52JkPGc7uYNnzwLqzJaI2");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$.QzRELeVg39tAkJceup5fu/T3lv/oQTcZaMhKC77oryLI4860FZyO");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "$2a$11$VX.66lQMROHE02mKTlHjSurAl00ZN.ZrMpjqswXQLABnooxwSGyjS");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 6,
                column: "Password",
                value: "$2a$11$3yGg0VMkhboq5ZkjPKPgl.8SkZVguDkd2uGI/kf0u5mRK9wr6orhS");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 7,
                column: "Password",
                value: "$2a$11$4Y9wf1hplDb/IznK6uZGqO5T86ZYNrDR17HLzqCNDCIXaFCPgD5g6");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 8,
                column: "Password",
                value: "$2a$11$V3//JercN529msTKF1VJL.CfnNx1WepexmN9RiAqDA4d3lV.9h6fO");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 9,
                column: "Password",
                value: "$2a$11$pWqLofFYPWXiA0VNuTribOSX9PdvY8Wba7P7f4bi/zLFA9.lWIfai");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 10,
                column: "Password",
                value: "$2a$11$UIhEYJTu87fpC9rnX.PAcOY/OPdhbPqrZ81XwMGYxvK3k57XAIAIO");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 11,
                column: "Password",
                value: "$2a$11$NETNrKtirlF7cNk3nmYItePEWF.1/IxmvmKpCTcHJZclz8l7O0mAG");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 12,
                column: "Password",
                value: "$2a$11$oMM16DItBt.RxdicYl59wOBVZRYFfokBrMYtOpJT69HJ4EprFWTU.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "CourseItem");

            migrationBuilder.UpdateData(
                table: "CourseItem",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "End", "Scheduled" },
                values: new object[] { new DateTime(2024, 1, 27, 15, 23, 35, 720, DateTimeKind.Utc).AddTicks(9836), new DateTime(2024, 1, 27, 18, 23, 35, 720, DateTimeKind.Utc).AddTicks(9842), new DateTime(2024, 1, 27, 15, 53, 35, 720, DateTimeKind.Utc).AddTicks(9837) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$zze7INAi/ENBWfpwGrIN8OvbmJqZqdCfuuuZAIIAaDV/7XGaTQ4bS");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$CvjgvrV2x8vX1rSZhyrPb.0hzX2F/qSL4EU1dKtFj9yeaGqd7GVzm");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$mN69wbQYRIherroQGUyTH.ziAddN2nsgHv74JrkBctBgxA2Qv4GYu");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$1uI8eeeAvgRapnsv9hgFZOTOHer7TLYEU1hq4b1NtmuTqmMWDapI2");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "$2a$11$KFV3EqNAQeMZgaTOYwVWQeWJV1M2ApRKdBa6jNp1iRPKt6Up7d0wK");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 6,
                column: "Password",
                value: "$2a$11$eDjE78HN.T6brV9jztHCq.U/nqPhGpc/gjNj48wX.KTyXrRN3YHau");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 7,
                column: "Password",
                value: "$2a$11$OTMHUZOh.UQ6mvw70/ukWu5Unmr.Ebr8I17NaK8i586ii6tjcoMri");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 8,
                column: "Password",
                value: "$2a$11$/HwIyifXjy3pUDi8o4dxEu4nwC/tJ0JPiOpQtUnLFERIOqYd9x73S");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 9,
                column: "Password",
                value: "$2a$11$AUmZ.vbILKVH5a9jKQA0QOjOe3ohJ5jnosw/y1MpBww3RQa5Gzvce");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 10,
                column: "Password",
                value: "$2a$11$RYEZMGLZQ/OPpMPca3uPQe55QZNHQVoFXu.lFhW76MrX0b0dViuH2");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 11,
                column: "Password",
                value: "$2a$11$doQqTv6Sass2aqBgt.GQk.PMh8vZTxygzgdP4tGhh2Y/6sGQwGGjq");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 12,
                column: "Password",
                value: "$2a$11$DyzTNT3s13DtDV9Ij9zgy.sAG0nXFMIQeLA2wwDlw/nGInmL3JZHC");
        }
    }
}

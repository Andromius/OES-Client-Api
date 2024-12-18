using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OESAppApi.Migrations
{
    /// <inheritdoc />
    public partial class UserQuizShouldShuffleQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShouldShuffleQuestions",
                table: "CourseItem",
                type: "boolean",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CourseItem",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "End", "Scheduled" },
                values: new object[] { new DateTime(2024, 12, 18, 16, 54, 58, 779, DateTimeKind.Utc).AddTicks(3983), new DateTime(2024, 12, 18, 19, 54, 58, 779, DateTimeKind.Utc).AddTicks(3991), new DateTime(2024, 12, 18, 17, 24, 58, 779, DateTimeKind.Utc).AddTicks(3983) });

            migrationBuilder.UpdateData(
                table: "CourseItem",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "End", "Scheduled" },
                values: new object[] { new DateTime(2024, 12, 18, 16, 54, 58, 778, DateTimeKind.Utc).AddTicks(8680), new DateTime(2024, 12, 18, 19, 54, 58, 778, DateTimeKind.Utc).AddTicks(8686), new DateTime(2024, 12, 18, 17, 24, 58, 778, DateTimeKind.Utc).AddTicks(8683) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$Gh6haQWVdf0scjRJtvscD.vYHNfhNhy91h43y1id5kvgaNxKrhKCy");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$6rjQwiggEbNq78UoW03Fse0VhUqLIEcF49K6iJQhGAxkall91hzB2");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$Y6CTBaeAEab0TuCgUM4lyuU2O5KTaohY18oSyQTfnUqycU1IE1e5.");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$hq6DWWGpxavANoBLbK1oAuilMb1xyTnZ5lc/A9Y3GWFXQhU24bLka");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "$2a$11$oZ57U5lUuatrWNzy2f/9peq/5jH19lzDYbjbuMskh09RP8xrzSJuy");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 6,
                column: "Password",
                value: "$2a$11$nBefL1dlA2VUwdTsC4JZmONPNT2dK8.DBd3YT/1YaJ2TVE4jCuWmC");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 7,
                column: "Password",
                value: "$2a$11$IedjV835TSKxxI1NOc5YmeaaXbNZhJ5AjQiFuKwp9Tk8zgqUQv9vS");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 8,
                column: "Password",
                value: "$2a$11$gEDIfL85HGGRIaXqNXc22.EU.L7ysBnWuQb89N4T7Z5Xkof/l3WwW");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 9,
                column: "Password",
                value: "$2a$11$4fzNtgarZ7rLJp28rGDbSeW8Wnj/3kcoSUX73SXMtArwPq4EDRRgS");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 10,
                column: "Password",
                value: "$2a$11$pdSV.cIuGNt3xKqHd5ffzOdpwk8NFyW4BKj5QTFXcHUx0/uPCyTiK");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 11,
                column: "Password",
                value: "$2a$11$ny3mzRGGYj4ygAl6bp5/zuOqHPe1JSUI4nWZLXwka.hJaV3eWL5re");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 12,
                column: "Password",
                value: "$2a$11$ljCbN8v31.NdZzNvZYArcuBZPzFZvOFrJRyVDLctbZ3KPQu1LUxFi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShouldShuffleQuestions",
                table: "CourseItem");

            migrationBuilder.UpdateData(
                table: "CourseItem",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "End", "Scheduled" },
                values: new object[] { new DateTime(2024, 5, 13, 15, 10, 56, 936, DateTimeKind.Utc).AddTicks(6940), new DateTime(2024, 5, 13, 18, 10, 56, 936, DateTimeKind.Utc).AddTicks(6943), new DateTime(2024, 5, 13, 15, 40, 56, 936, DateTimeKind.Utc).AddTicks(6941) });

            migrationBuilder.UpdateData(
                table: "CourseItem",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "End", "Scheduled" },
                values: new object[] { new DateTime(2024, 5, 13, 15, 10, 56, 936, DateTimeKind.Utc).AddTicks(2668), new DateTime(2024, 5, 13, 18, 10, 56, 936, DateTimeKind.Utc).AddTicks(2674), new DateTime(2024, 5, 13, 15, 40, 56, 936, DateTimeKind.Utc).AddTicks(2671) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$xtGQPt3tcheznz9PPM2Uien04AxvQhn7Mdbq4BEsYR6gmDc7MKOyi");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$n/lHM1VPD5gVsTNz0TqxLetoJRaZsQrssACW8Bx7Qyl25lY.BuIIK");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$reTFqzHH61liIv2xBkmRxe91oOjQevuPWnpHTgvf9SDaLsboXuxla");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$PS9ClR00k0lyx1yW9wduMefbELx3KaxmrCfYc/IddXMHDZ8PFf.0K");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "$2a$11$jxMpfiC3q5M5lYKwVcHaQOmkSe9zNjYYpQi0.cKOrfg.UuCZzUkyG");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 6,
                column: "Password",
                value: "$2a$11$Cwrdi0jcrAxB8EpGPGqxJOoUhJAAeRH6vOjKaggWcF/JEH87tz.xy");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 7,
                column: "Password",
                value: "$2a$11$WhoSWQYNoeJKCwl9XHlzKuZDpcwauEqTPQ2m9Xwlnj.A/afdymmGi");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 8,
                column: "Password",
                value: "$2a$11$gXio2pwE181eJjg2KuZAUOsQ6KJdLjOKC4aWZ/CkOUN5sULnY4XXW");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 9,
                column: "Password",
                value: "$2a$11$sT2qal8j1dgeIjC4uXmwDO2SqgX/JFpiat92Hif.dFfK.nVj/FykC");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 10,
                column: "Password",
                value: "$2a$11$/J/G7Xm4YULmkgb6DP8gn.FStiaXkF0bh3PLzfBQybScz8btmVlzy");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 11,
                column: "Password",
                value: "$2a$11$5GnbH8H.YXYltXVNqnK6FOBtOKx/MT2epd9AbPtzksPgVh5vabpKG");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 12,
                column: "Password",
                value: "$2a$11$8JLZLwD/DO3ygu5CMhPTgukLzpnTl1RSHKW3ZZomwfj7meghJXNDe");
        }
    }
}

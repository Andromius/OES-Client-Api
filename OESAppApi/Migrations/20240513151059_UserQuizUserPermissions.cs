using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OESAppApi.Migrations
{
    /// <inheritdoc />
    public partial class UserQuizUserPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserQuizUserPermission",
                columns: table => new
                {
                    UserQuizId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Permission = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserQuizUserPermission", x => new { x.UserQuizId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserQuizUserPermission_CourseItem_UserQuizId",
                        column: x => x.UserQuizId,
                        principalTable: "CourseItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserQuizUserPermission_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_UserQuizUserPermission_UserId",
                table: "UserQuizUserPermission",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserQuizUserPermission");

            migrationBuilder.UpdateData(
                table: "CourseItem",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "End", "Scheduled" },
                values: new object[] { new DateTime(2024, 4, 21, 19, 32, 54, 611, DateTimeKind.Utc).AddTicks(8642), new DateTime(2024, 4, 21, 22, 32, 54, 611, DateTimeKind.Utc).AddTicks(8647), new DateTime(2024, 4, 21, 20, 2, 54, 611, DateTimeKind.Utc).AddTicks(8645) });

            migrationBuilder.UpdateData(
                table: "CourseItem",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "End", "Scheduled" },
                values: new object[] { new DateTime(2024, 4, 21, 19, 32, 54, 611, DateTimeKind.Utc).AddTicks(4496), new DateTime(2024, 4, 21, 22, 32, 54, 611, DateTimeKind.Utc).AddTicks(4503), new DateTime(2024, 4, 21, 20, 2, 54, 611, DateTimeKind.Utc).AddTicks(4498) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$XcE4G5a.GZu63g8RryuHIuqL8yBZjlYhk6jyVr65DzjqWCDaj.vxS");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$wM0VEpt0GCxTO.2v1nfN6exFf3ix4TOyDy9p.u/sruBH3aUBPtQ4S");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$SWb9PjPmgOQy5ZQUeFj1eOMWhmDZi1q.ymNVX3vO6tyAffmWe.Dpa");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$qx4mTEd.Q7CSQW8HENIByeAzhjmU7sbUy2mG519z5KQ017Yri7tDu");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "$2a$11$N88lwI.AkgJzbWEiGWFSBONdwZzhFI2C23/lRYlmpD4nT8eRt.d12");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 6,
                column: "Password",
                value: "$2a$11$4H8YgQT6WGLbyXOiF9J9HuzrtrmYgmQ6gVW8IYXv398OciPyAcu1S");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 7,
                column: "Password",
                value: "$2a$11$STsRb1A.Dp6VvrVBKGW21eOIIRpnkvUzOSTXneky8IV9SOK.aI7Am");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 8,
                column: "Password",
                value: "$2a$11$vp.AY2jhpWpCqKl2M3ey0.VkZYnP4iQ9cGzQhs5r1EDuN2QVIStju");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 9,
                column: "Password",
                value: "$2a$11$hhjE9cz6ii4hDTgQjJW01.U6r9aeNocxPVWmjEaplvQZoqy0U1f4u");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 10,
                column: "Password",
                value: "$2a$11$aIJtiwdyO5jKwddT2JXvHO1muTJEg6wqjzodvdsoP5KC.3KqmjOHy");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 11,
                column: "Password",
                value: "$2a$11$BqcEUWJKKox5j0nWJ.m0Cu8Sttq7kSO0lYcwaDPn9WSKm0uWpIayq");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 12,
                column: "Password",
                value: "$2a$11$NG0JIG.wqJisghHeBkjc6u6P4SpuVZF9GJYSRPWNc77cyNpzhwjDi");
        }
    }
}

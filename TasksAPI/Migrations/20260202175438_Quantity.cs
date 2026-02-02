using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasksAPI.Migrations
{
    /// <inheritdoc />
    public partial class Quantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "GoodsTypesInstances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(5730), new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(5731) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(5956), new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(5956) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(5957), new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(5957) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(5958), new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(5958) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(6122), new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(6122) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(6337), new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(6337) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(6338), new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(6338) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(6339), new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(6339) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(6340), new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(6340) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(6341), new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(6342) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(2726), new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(2845) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(3103), new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(3104) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(3105), new DateTime(2026, 2, 2, 17, 54, 38, 652, DateTimeKind.Utc).AddTicks(3105) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "GoodsTypesInstances");

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(3343), new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(3343) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(3566), new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(3566) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(3567), new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(3567) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(3568), new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(3568) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(3765), new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(3765) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(3979), new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(3979) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(3980), new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(3980) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(3981), new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(3981) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(3982), new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(3982) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(3983), new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(3983) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(167), new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(288) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(607), new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(607) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(609), new DateTime(2026, 2, 2, 17, 32, 40, 879, DateTimeKind.Utc).AddTicks(609) });
        }
    }
}

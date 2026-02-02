using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasksAPI.Migrations
{
    /// <inheritdoc />
    public partial class InventoryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InventoryKey",
                table: "GoodModelBaseType",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InventoryKey",
                table: "GoodModelBaseType");

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 0, 32, 606, DateTimeKind.Utc).AddTicks(2350), new DateTime(2026, 2, 2, 17, 0, 32, 606, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 0, 32, 606, DateTimeKind.Utc).AddTicks(2570), new DateTime(2026, 2, 2, 17, 0, 32, 606, DateTimeKind.Utc).AddTicks(2570) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 0, 32, 606, DateTimeKind.Utc).AddTicks(2571), new DateTime(2026, 2, 2, 17, 0, 32, 606, DateTimeKind.Utc).AddTicks(2572) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 0, 32, 606, DateTimeKind.Utc).AddTicks(2572), new DateTime(2026, 2, 2, 17, 0, 32, 606, DateTimeKind.Utc).AddTicks(2572) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 0, 32, 606, DateTimeKind.Utc).AddTicks(2771), new DateTime(2026, 2, 2, 17, 0, 32, 606, DateTimeKind.Utc).AddTicks(2772) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 0, 32, 606, DateTimeKind.Utc).AddTicks(2986), new DateTime(2026, 2, 2, 17, 0, 32, 606, DateTimeKind.Utc).AddTicks(2986) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 0, 32, 606, DateTimeKind.Utc).AddTicks(2987), new DateTime(2026, 2, 2, 17, 0, 32, 606, DateTimeKind.Utc).AddTicks(2987) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 0, 32, 606, DateTimeKind.Utc).AddTicks(2988), new DateTime(2026, 2, 2, 17, 0, 32, 606, DateTimeKind.Utc).AddTicks(2988) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 0, 32, 606, DateTimeKind.Utc).AddTicks(2989), new DateTime(2026, 2, 2, 17, 0, 32, 606, DateTimeKind.Utc).AddTicks(2989) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 0, 32, 606, DateTimeKind.Utc).AddTicks(2990), new DateTime(2026, 2, 2, 17, 0, 32, 606, DateTimeKind.Utc).AddTicks(2990) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 0, 32, 605, DateTimeKind.Utc).AddTicks(9311), new DateTime(2026, 2, 2, 17, 0, 32, 605, DateTimeKind.Utc).AddTicks(9429) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 0, 32, 605, DateTimeKind.Utc).AddTicks(9684), new DateTime(2026, 2, 2, 17, 0, 32, 605, DateTimeKind.Utc).AddTicks(9684) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 17, 0, 32, 605, DateTimeKind.Utc).AddTicks(9685), new DateTime(2026, 2, 2, 17, 0, 32, 605, DateTimeKind.Utc).AddTicks(9686) });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasksAPI.Migrations
{
    /// <inheritdoc />
    public partial class RegisterNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegisterNumber",
                table: "CashRegisterEntity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(6325), new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(6325) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(6574), new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(6574) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(6575), new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(6575) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(6576), new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(6576) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(6761), new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(6762) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(7000), new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(7001) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(7002), new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(7002) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(7003), new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(7003) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(7004), new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(7004) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(7005), new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(7006) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(2880), new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(3015) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(3341), new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(3341) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(3343), new DateTime(2026, 3, 5, 18, 32, 38, 414, DateTimeKind.Utc).AddTicks(3343) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisterNumber",
                table: "CashRegisterEntity");

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(3828), new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(3828) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(4047), new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(4047) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(4048), new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(4048) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(4049), new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(4049) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(4211), new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(4212) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(4462), new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(4462) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(4463), new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(4463) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(4464), new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(4464) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(4465), new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(4465) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(4466), new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(4466) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(932), new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(1072) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(1331), new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(1332) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(1333), new DateTime(2026, 2, 16, 21, 2, 38, 42, DateTimeKind.Utc).AddTicks(1333) });
        }
    }
}

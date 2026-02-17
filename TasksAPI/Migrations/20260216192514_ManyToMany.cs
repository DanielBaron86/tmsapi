using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasksAPI.Migrations
{
    /// <inheritdoc />
    public partial class ManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(7317), new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(7318) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(7543), new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(7543) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(7544), new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(7544) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(7545), new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(7545) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(7718), new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(7718) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(7936), new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(7937) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(7938), new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(7938) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(7939), new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(7939) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(7940), new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(7940) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(7941), new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(7941) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(4198), new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(4320) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(4623), new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(4623) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(4624), new DateTime(2026, 2, 16, 19, 25, 14, 32, DateTimeKind.Utc).AddTicks(4624) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 6, 10, 35, 11, 679, DateTimeKind.Utc).AddTicks(2391), new DateTime(2026, 2, 6, 10, 35, 11, 679, DateTimeKind.Utc).AddTicks(2391) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 6, 10, 35, 11, 679, DateTimeKind.Utc).AddTicks(2617), new DateTime(2026, 2, 6, 10, 35, 11, 679, DateTimeKind.Utc).AddTicks(2617) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 6, 10, 35, 11, 679, DateTimeKind.Utc).AddTicks(2618), new DateTime(2026, 2, 6, 10, 35, 11, 679, DateTimeKind.Utc).AddTicks(2618) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 6, 10, 35, 11, 679, DateTimeKind.Utc).AddTicks(2619), new DateTime(2026, 2, 6, 10, 35, 11, 679, DateTimeKind.Utc).AddTicks(2619) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 6, 10, 35, 11, 679, DateTimeKind.Utc).AddTicks(2794), new DateTime(2026, 2, 6, 10, 35, 11, 679, DateTimeKind.Utc).AddTicks(2794) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 6, 10, 35, 11, 679, DateTimeKind.Utc).AddTicks(3007), new DateTime(2026, 2, 6, 10, 35, 11, 679, DateTimeKind.Utc).AddTicks(3007) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 6, 10, 35, 11, 679, DateTimeKind.Utc).AddTicks(3008), new DateTime(2026, 2, 6, 10, 35, 11, 679, DateTimeKind.Utc).AddTicks(3008) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 6, 10, 35, 11, 679, DateTimeKind.Utc).AddTicks(3009), new DateTime(2026, 2, 6, 10, 35, 11, 679, DateTimeKind.Utc).AddTicks(3009) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 6, 10, 35, 11, 679, DateTimeKind.Utc).AddTicks(3010), new DateTime(2026, 2, 6, 10, 35, 11, 679, DateTimeKind.Utc).AddTicks(3011) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 6, 10, 35, 11, 679, DateTimeKind.Utc).AddTicks(3011), new DateTime(2026, 2, 6, 10, 35, 11, 679, DateTimeKind.Utc).AddTicks(3012) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 6, 10, 35, 11, 678, DateTimeKind.Utc).AddTicks(9239), new DateTime(2026, 2, 6, 10, 35, 11, 678, DateTimeKind.Utc).AddTicks(9356) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 6, 10, 35, 11, 678, DateTimeKind.Utc).AddTicks(9612), new DateTime(2026, 2, 6, 10, 35, 11, 678, DateTimeKind.Utc).AddTicks(9612) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 6, 10, 35, 11, 678, DateTimeKind.Utc).AddTicks(9614), new DateTime(2026, 2, 6, 10, 35, 11, 678, DateTimeKind.Utc).AddTicks(9614) });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasksAPI.Migrations
{
    /// <inheritdoc />
    public partial class ProcGoodType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GoodType",
                table: "TasksEntitiesProcurements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 56, 35, 125, DateTimeKind.Utc).AddTicks(206), new DateTime(2026, 2, 4, 9, 56, 35, 125, DateTimeKind.Utc).AddTicks(207) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 56, 35, 125, DateTimeKind.Utc).AddTicks(428), new DateTime(2026, 2, 4, 9, 56, 35, 125, DateTimeKind.Utc).AddTicks(428) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 56, 35, 125, DateTimeKind.Utc).AddTicks(429), new DateTime(2026, 2, 4, 9, 56, 35, 125, DateTimeKind.Utc).AddTicks(429) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 56, 35, 125, DateTimeKind.Utc).AddTicks(430), new DateTime(2026, 2, 4, 9, 56, 35, 125, DateTimeKind.Utc).AddTicks(430) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 56, 35, 125, DateTimeKind.Utc).AddTicks(603), new DateTime(2026, 2, 4, 9, 56, 35, 125, DateTimeKind.Utc).AddTicks(603) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 56, 35, 125, DateTimeKind.Utc).AddTicks(819), new DateTime(2026, 2, 4, 9, 56, 35, 125, DateTimeKind.Utc).AddTicks(819) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 56, 35, 125, DateTimeKind.Utc).AddTicks(820), new DateTime(2026, 2, 4, 9, 56, 35, 125, DateTimeKind.Utc).AddTicks(821) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 56, 35, 125, DateTimeKind.Utc).AddTicks(821), new DateTime(2026, 2, 4, 9, 56, 35, 125, DateTimeKind.Utc).AddTicks(822) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 56, 35, 125, DateTimeKind.Utc).AddTicks(823), new DateTime(2026, 2, 4, 9, 56, 35, 125, DateTimeKind.Utc).AddTicks(823) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 56, 35, 125, DateTimeKind.Utc).AddTicks(824), new DateTime(2026, 2, 4, 9, 56, 35, 125, DateTimeKind.Utc).AddTicks(824) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 56, 35, 124, DateTimeKind.Utc).AddTicks(7125), new DateTime(2026, 2, 4, 9, 56, 35, 124, DateTimeKind.Utc).AddTicks(7247) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 56, 35, 124, DateTimeKind.Utc).AddTicks(7501), new DateTime(2026, 2, 4, 9, 56, 35, 124, DateTimeKind.Utc).AddTicks(7502) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 56, 35, 124, DateTimeKind.Utc).AddTicks(7503), new DateTime(2026, 2, 4, 9, 56, 35, 124, DateTimeKind.Utc).AddTicks(7503) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoodType",
                table: "TasksEntitiesProcurements");

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(9204), new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(9204) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(9428), new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(9428) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(9429), new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(9429) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(9430), new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(9430) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(9597), new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(9598) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(9809), new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(9809) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(9810), new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(9811) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(9811), new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(9812) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(9813), new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(9813) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(9814), new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(9814) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(6147), new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(6266) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(6528), new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(6528) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(6529), new DateTime(2026, 2, 3, 20, 49, 49, 217, DateTimeKind.Utc).AddTicks(6529) });
        }
    }
}

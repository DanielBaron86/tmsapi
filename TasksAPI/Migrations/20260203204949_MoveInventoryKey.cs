using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasksAPI.Migrations
{
    /// <inheritdoc />
    public partial class MoveInventoryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InventoryKey",
                table: "GoodModelBaseType");

            migrationBuilder.AlterColumn<string>(
                name: "serialNumber",
                table: "GoodsTypesInstances",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "InventoryKey",
                table: "GoodsTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InventoryKey",
                table: "GoodsTypes");

            migrationBuilder.AlterColumn<string>(
                name: "serialNumber",
                table: "GoodsTypesInstances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
                values: new object[] { new DateTime(2026, 2, 3, 19, 48, 51, 722, DateTimeKind.Utc).AddTicks(1732), new DateTime(2026, 2, 3, 19, 48, 51, 722, DateTimeKind.Utc).AddTicks(1732) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 19, 48, 51, 722, DateTimeKind.Utc).AddTicks(1977), new DateTime(2026, 2, 3, 19, 48, 51, 722, DateTimeKind.Utc).AddTicks(1977) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 19, 48, 51, 722, DateTimeKind.Utc).AddTicks(1978), new DateTime(2026, 2, 3, 19, 48, 51, 722, DateTimeKind.Utc).AddTicks(1979) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 19, 48, 51, 722, DateTimeKind.Utc).AddTicks(1979), new DateTime(2026, 2, 3, 19, 48, 51, 722, DateTimeKind.Utc).AddTicks(1979) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 19, 48, 51, 722, DateTimeKind.Utc).AddTicks(2148), new DateTime(2026, 2, 3, 19, 48, 51, 722, DateTimeKind.Utc).AddTicks(2148) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 19, 48, 51, 722, DateTimeKind.Utc).AddTicks(2495), new DateTime(2026, 2, 3, 19, 48, 51, 722, DateTimeKind.Utc).AddTicks(2495) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 19, 48, 51, 722, DateTimeKind.Utc).AddTicks(2496), new DateTime(2026, 2, 3, 19, 48, 51, 722, DateTimeKind.Utc).AddTicks(2497) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 19, 48, 51, 722, DateTimeKind.Utc).AddTicks(2497), new DateTime(2026, 2, 3, 19, 48, 51, 722, DateTimeKind.Utc).AddTicks(2498) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 19, 48, 51, 722, DateTimeKind.Utc).AddTicks(2498), new DateTime(2026, 2, 3, 19, 48, 51, 722, DateTimeKind.Utc).AddTicks(2499) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 19, 48, 51, 722, DateTimeKind.Utc).AddTicks(2499), new DateTime(2026, 2, 3, 19, 48, 51, 722, DateTimeKind.Utc).AddTicks(2500) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 19, 48, 51, 721, DateTimeKind.Utc).AddTicks(8670), new DateTime(2026, 2, 3, 19, 48, 51, 721, DateTimeKind.Utc).AddTicks(8791) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 19, 48, 51, 721, DateTimeKind.Utc).AddTicks(9048), new DateTime(2026, 2, 3, 19, 48, 51, 721, DateTimeKind.Utc).AddTicks(9049) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 3, 19, 48, 51, 721, DateTimeKind.Utc).AddTicks(9050), new DateTime(2026, 2, 3, 19, 48, 51, 721, DateTimeKind.Utc).AddTicks(9050) });
        }
    }
}

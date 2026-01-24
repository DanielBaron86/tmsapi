using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasksAPI.Migrations
{
    /// <inheritdoc />
    public partial class GoodModelBaseType2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoodModelBaseTypeId",
                table: "GoodModelBaseType");

            migrationBuilder.UpdateData(
                table: "GoodModelBaseType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(5065), new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(5066) });

            migrationBuilder.UpdateData(
                table: "GoodModelBaseType",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(5223), new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(5224) });

            migrationBuilder.UpdateData(
                table: "GoodsTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(5351), new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(5352) });

            migrationBuilder.UpdateData(
                table: "GoodsTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(5581), new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(5582) });

            migrationBuilder.UpdateData(
                table: "GoodsTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(5583), new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(5584) });

            migrationBuilder.UpdateData(
                table: "GoodsTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(5585), new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(5586) });

            migrationBuilder.UpdateData(
                table: "GoodsTypes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(5587), new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(5588) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(4214), new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(4221) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(4466), new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(4467) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(4468), new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(4469) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(4470), new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(4470) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(4647), new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(4648) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(4863), new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(4863) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(4865), new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(4865) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(4867), new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(4867) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(4868), new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(4869) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(4870), new DateTime(2026, 1, 24, 9, 55, 46, 570, DateTimeKind.Local).AddTicks(4871) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedDate",
                value: new DateTime(2026, 1, 24, 9, 55, 46, 565, DateTimeKind.Local).AddTicks(3839));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 24, 9, 55, 46, 569, DateTimeKind.Local).AddTicks(7904), new DateTime(2026, 1, 24, 9, 55, 46, 569, DateTimeKind.Local).AddTicks(7916) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 24, 9, 55, 46, 569, DateTimeKind.Local).AddTicks(7918), new DateTime(2026, 1, 24, 9, 55, 46, 569, DateTimeKind.Local).AddTicks(7919) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GoodModelBaseTypeId",
                table: "GoodModelBaseType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "GoodModelBaseType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "GoodModelBaseTypeId", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2849), 1, new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2850) });

            migrationBuilder.UpdateData(
                table: "GoodModelBaseType",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "GoodModelBaseTypeId", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(3059), 2, new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(3059) });

            migrationBuilder.UpdateData(
                table: "GoodsTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(3188), new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(3189) });

            migrationBuilder.UpdateData(
                table: "GoodsTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(3391), new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(3392) });

            migrationBuilder.UpdateData(
                table: "GoodsTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(3393), new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(3394) });

            migrationBuilder.UpdateData(
                table: "GoodsTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(3395), new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(3395) });

            migrationBuilder.UpdateData(
                table: "GoodsTypes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(3397), new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(3397) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2007), new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2013) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2253), new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2254) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2255), new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2255) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2257), new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2257) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2430), new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2431) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2648), new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2649) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2650), new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2651) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2652), new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2653) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2654), new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2654) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2656), new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2656) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedDate",
                value: new DateTime(2026, 1, 23, 16, 12, 36, 594, DateTimeKind.Local).AddTicks(1753));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 12, 36, 598, DateTimeKind.Local).AddTicks(5492), new DateTime(2026, 1, 23, 16, 12, 36, 598, DateTimeKind.Local).AddTicks(5497) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 12, 36, 598, DateTimeKind.Local).AddTicks(5499), new DateTime(2026, 1, 23, 16, 12, 36, 598, DateTimeKind.Local).AddTicks(5500) });
        }
    }
}

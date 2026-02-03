using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasksAPI.Migrations
{
    /// <inheritdoc />
    public partial class GoodsInstancesView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 20, 35, 52, 219, DateTimeKind.Utc).AddTicks(9408), new DateTime(2026, 2, 2, 20, 35, 52, 219, DateTimeKind.Utc).AddTicks(9408) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 20, 35, 52, 219, DateTimeKind.Utc).AddTicks(9636), new DateTime(2026, 2, 2, 20, 35, 52, 219, DateTimeKind.Utc).AddTicks(9636) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 20, 35, 52, 219, DateTimeKind.Utc).AddTicks(9637), new DateTime(2026, 2, 2, 20, 35, 52, 219, DateTimeKind.Utc).AddTicks(9637) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 20, 35, 52, 219, DateTimeKind.Utc).AddTicks(9638), new DateTime(2026, 2, 2, 20, 35, 52, 219, DateTimeKind.Utc).AddTicks(9638) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 20, 35, 52, 219, DateTimeKind.Utc).AddTicks(9802), new DateTime(2026, 2, 2, 20, 35, 52, 219, DateTimeKind.Utc).AddTicks(9803) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 20, 35, 52, 220, DateTimeKind.Utc).AddTicks(15), new DateTime(2026, 2, 2, 20, 35, 52, 220, DateTimeKind.Utc).AddTicks(15) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 20, 35, 52, 220, DateTimeKind.Utc).AddTicks(16), new DateTime(2026, 2, 2, 20, 35, 52, 220, DateTimeKind.Utc).AddTicks(17) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 20, 35, 52, 220, DateTimeKind.Utc).AddTicks(17), new DateTime(2026, 2, 2, 20, 35, 52, 220, DateTimeKind.Utc).AddTicks(18) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 20, 35, 52, 220, DateTimeKind.Utc).AddTicks(18), new DateTime(2026, 2, 2, 20, 35, 52, 220, DateTimeKind.Utc).AddTicks(19) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 20, 35, 52, 220, DateTimeKind.Utc).AddTicks(19), new DateTime(2026, 2, 2, 20, 35, 52, 220, DateTimeKind.Utc).AddTicks(20) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 20, 35, 52, 219, DateTimeKind.Utc).AddTicks(6346), new DateTime(2026, 2, 2, 20, 35, 52, 219, DateTimeKind.Utc).AddTicks(6466) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 20, 35, 52, 219, DateTimeKind.Utc).AddTicks(6783), new DateTime(2026, 2, 2, 20, 35, 52, 219, DateTimeKind.Utc).AddTicks(6784) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 20, 35, 52, 219, DateTimeKind.Utc).AddTicks(6785), new DateTime(2026, 2, 2, 20, 35, 52, 219, DateTimeKind.Utc).AddTicks(6785) });
        }
    }
}

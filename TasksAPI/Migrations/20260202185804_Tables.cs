using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasksAPI.Migrations
{
    /// <inheritdoc />
    public partial class Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsTypes_GoodModelBaseType_GoodModelId",
                table: "GoodsTypes");

            migrationBuilder.RenameColumn(
                name: "GoodModelId",
                table: "GoodsTypes",
                newName: "GoodBaseId");

            migrationBuilder.RenameIndex(
                name: "IX_GoodsTypes_GoodModelId",
                table: "GoodsTypes",
                newName: "IX_GoodsTypes_GoodBaseId");

            migrationBuilder.AddColumn<string>(
                name: "FromLocationName",
                table: "TasksEntitiesTransfer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToLocationName",
                table: "TasksEntitiesTransfer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(3487), new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(3487) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(3711), new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(3711) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(3712), new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(3712) });

            migrationBuilder.UpdateData(
                table: "LocationEntity",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(3713), new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(3713) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(3877), new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(3877) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(4092), new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(4092) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(4094), new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(4094) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(4095), new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(4095) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(4096), new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(4096) });

            migrationBuilder.UpdateData(
                table: "LocationTypesInstances",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(4097), new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(4097) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(361), new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(482) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(854), new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(854) });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(855), new DateTime(2026, 2, 2, 18, 58, 3, 728, DateTimeKind.Utc).AddTicks(855) });

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsTypes_GoodModelBaseType_GoodBaseId",
                table: "GoodsTypes",
                column: "GoodBaseId",
                principalTable: "GoodModelBaseType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsTypes_GoodModelBaseType_GoodBaseId",
                table: "GoodsTypes");

            migrationBuilder.DropColumn(
                name: "FromLocationName",
                table: "TasksEntitiesTransfer");

            migrationBuilder.DropColumn(
                name: "ToLocationName",
                table: "TasksEntitiesTransfer");

            migrationBuilder.RenameColumn(
                name: "GoodBaseId",
                table: "GoodsTypes",
                newName: "GoodModelId");

            migrationBuilder.RenameIndex(
                name: "IX_GoodsTypes_GoodBaseId",
                table: "GoodsTypes",
                newName: "IX_GoodsTypes_GoodModelId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsTypes_GoodModelBaseType_GoodModelId",
                table: "GoodsTypes",
                column: "GoodModelId",
                principalTable: "GoodModelBaseType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasksAPI.Migrations
{
    /// <inheritdoc />
    public partial class OneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationTypesInstancesTasksEntitiesProcurements");

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

            migrationBuilder.CreateIndex(
                name: "IX_TasksEntitiesProcurements_Location",
                table: "TasksEntitiesProcurements",
                column: "Location");

            migrationBuilder.AddForeignKey(
                name: "FK_TasksEntitiesProcurements_LocationTypesInstances_Location",
                table: "TasksEntitiesProcurements",
                column: "Location",
                principalTable: "LocationTypesInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TasksEntitiesProcurements_LocationTypesInstances_Location",
                table: "TasksEntitiesProcurements");

            migrationBuilder.DropIndex(
                name: "IX_TasksEntitiesProcurements_Location",
                table: "TasksEntitiesProcurements");

            migrationBuilder.CreateTable(
                name: "LocationTypesInstancesTasksEntitiesProcurements",
                columns: table => new
                {
                    Location = table.Column<int>(type: "int", nullable: false),
                    LocationTypesInstancesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationTypesInstancesTasksEntitiesProcurements", x => new { x.Location, x.LocationTypesInstancesId });
                    table.ForeignKey(
                        name: "FK_LocationTypesInstancesTasksEntitiesProcurements_LocationTypesInstances_LocationTypesInstancesId",
                        column: x => x.LocationTypesInstancesId,
                        principalTable: "LocationTypesInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationTypesInstancesTasksEntitiesProcurements_TasksEntitiesProcurements_Location",
                        column: x => x.Location,
                        principalTable: "TasksEntitiesProcurements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_LocationTypesInstancesTasksEntitiesProcurements_LocationTypesInstancesId",
                table: "LocationTypesInstancesTasksEntitiesProcurements",
                column: "LocationTypesInstancesId");
        }
    }
}

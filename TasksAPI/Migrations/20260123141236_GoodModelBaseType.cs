using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TasksAPI.Migrations
{
    /// <inheritdoc />
    public partial class GoodModelBaseType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GoodModelBaseType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoodModelBaseTypeId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodModelBaseType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemMovementEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    goodId = table.Column<int>(type: "int", nullable: false),
                    FromLocation = table.Column<int>(type: "int", nullable: false),
                    ToLocation = table.Column<int>(type: "int", nullable: false),
                    FromStatus = table.Column<int>(type: "int", nullable: false),
                    ToStatus = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemMovementEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationEntity", x => x.Id);
                    table.UniqueConstraint("AK_LocationEntity_LocationType", x => x.LocationType);
                });

            migrationBuilder.CreateTable(
                name: "ReportsEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descrption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportType = table.Column<int>(type: "int", nullable: false),
                    ReportMode = table.Column<int>(type: "int", nullable: false),
                    ReportStatus = table.Column<int>(type: "int", nullable: false),
                    Params = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportsEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreCartsEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clerktId = table.Column<int>(type: "int", nullable: false),
                    storeLocation = table.Column<int>(type: "int", nullable: false),
                    clientId = table.Column<int>(type: "int", nullable: false),
                    SessionID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Paid = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Remaining = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreCartsEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserTypeId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.Id);
                    table.UniqueConstraint("AK_UserTypes_UserTypeId", x => x.UserTypeId);
                });

            migrationBuilder.CreateTable(
                name: "GoodsTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoodModelId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsTypes_GoodModelBaseType_GoodModelId",
                        column: x => x.GoodModelId,
                        principalTable: "GoodModelBaseType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationTypesInstances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationTypeID = table.Column<int>(type: "int", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationTypesInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationTypesInstances_LocationEntity_LocationTypeID",
                        column: x => x.LocationTypeID,
                        principalTable: "LocationEntity",
                        principalColumn: "LocationType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportsEntitiesResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportID = table.Column<int>(type: "int", nullable: false),
                    RunDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportResults = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportsEntitiesResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportsEntitiesResults_ReportsEntities_ReportID",
                        column: x => x.ReportID,
                        principalTable: "ReportsEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreCartsEntityDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    OperationType = table.Column<int>(type: "int", nullable: false),
                    GoodId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreCartsEntityDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreCartsEntityDetails_StoreCartsEntity_CartId",
                        column: x => x.CartId,
                        principalTable: "StoreCartsEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserTypeId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_UserTypes_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserTypes",
                        principalColumn: "UserTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserTypeId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserTypes_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserTypes",
                        principalColumn: "UserTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CashRegisterEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashRegisterEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashRegisterEntity_LocationTypesInstances_LocationID",
                        column: x => x.LocationID,
                        principalTable: "LocationTypesInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsTypesInstances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoodModelId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    serialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsTypesInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsTypesInstances_GoodsTypes_GoodModelId",
                        column: x => x.GoodModelId,
                        principalTable: "GoodsTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsTypesInstances_LocationTypesInstances_LocationId",
                        column: x => x.LocationId,
                        principalTable: "LocationTypesInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountsGoodsEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    GoodId = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsGoodsEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountsGoodsEntity_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TasksEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskType = table.Column<int>(type: "int", nullable: false),
                    TaskStatus = table.Column<int>(type: "int", nullable: false),
                    userID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasksEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TasksEntities_Users_userID",
                        column: x => x.userID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CashRegisterEntitySessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionStatus = table.Column<int>(type: "int", nullable: false),
                    AssignedClerk = table.Column<int>(type: "int", nullable: false),
                    CashRegisterID = table.Column<int>(type: "int", nullable: false),
                    OpenHour = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CloseHour = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashRegisterEntitySessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashRegisterEntitySessions_CashRegisterEntity_CashRegisterID",
                        column: x => x.CashRegisterID,
                        principalTable: "CashRegisterEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashRegisterEntitySessions_Users_AssignedClerk",
                        column: x => x.AssignedClerk,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TasksEntitiesProcurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskID = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<int>(type: "int", nullable: false),
                    GoodTypeID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    RemainingQuantity = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasksEntitiesProcurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TasksEntitiesProcurements_GoodsTypes_GoodTypeID",
                        column: x => x.GoodTypeID,
                        principalTable: "GoodsTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TasksEntitiesProcurements_TasksEntities_TaskID",
                        column: x => x.TaskID,
                        principalTable: "TasksEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TasksEntitiesTransfer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskID = table.Column<int>(type: "int", nullable: false),
                    GoodID = table.Column<int>(type: "int", nullable: false),
                    serialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromLocation = table.Column<int>(type: "int", nullable: false),
                    ToLocation = table.Column<int>(type: "int", nullable: false),
                    TaskStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasksEntitiesTransfer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TasksEntitiesTransfer_GoodsTypesInstances_GoodID",
                        column: x => x.GoodID,
                        principalTable: "GoodsTypesInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TasksEntitiesTransfer_LocationTypesInstances_ToLocation",
                        column: x => x.ToLocation,
                        principalTable: "LocationTypesInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TasksEntitiesTransfer_TasksEntities_TaskID",
                        column: x => x.TaskID,
                        principalTable: "TasksEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "GoodModelBaseType",
                columns: new[] { "Id", "CreatedDate", "Description", "GoodModelBaseTypeId", "Manufacturer", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2849), "Smartphone", 1, "Samsung", new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2850) },
                    { 2, new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(3059), "Smartphone", 2, "Apple", new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(3059) }
                });

            migrationBuilder.InsertData(
                table: "LocationEntity",
                columns: new[] { "Id", "CreatedDate", "Description", "LocationType", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2007), "Warehouse", 1, new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2013) },
                    { 2, new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2253), "STORE", 2, new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2254) },
                    { 3, new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2255), "CLIENT", 3, new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2255) },
                    { 4, new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2257), "SUPPLIER", 4, new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2257) }
                });

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "Id", "CreatedDate", "Description", "UpdatedDate", "UserTypeId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Client", new DateTime(2026, 1, 23, 16, 12, 36, 594, DateTimeKind.Local).AddTicks(1753), 2 },
                    { 2, new DateTime(2026, 1, 23, 16, 12, 36, 598, DateTimeKind.Local).AddTicks(5492), "Clerk", new DateTime(2026, 1, 23, 16, 12, 36, 598, DateTimeKind.Local).AddTicks(5497), 3 },
                    { 3, new DateTime(2026, 1, 23, 16, 12, 36, 598, DateTimeKind.Local).AddTicks(5499), "Supervisor", new DateTime(2026, 1, 23, 16, 12, 36, 598, DateTimeKind.Local).AddTicks(5500), 4 }
                });

            migrationBuilder.InsertData(
                table: "GoodsTypes",
                columns: new[] { "Id", "CreatedDate", "Description", "GoodModelId", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(3188), "Samsung Smartphone", 1, "A53", new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(3189) },
                    { 2, new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(3391), "Samsung Smartphone", 1, "ZFLIP", new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(3392) },
                    { 3, new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(3393), "Samsung Smartphone", 1, "M14", new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(3394) },
                    { 4, new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(3395), "Samsung Smartphone", 1, "S21", new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(3395) },
                    { 5, new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(3397), "Apple Smartphone", 2, "Apple 15", new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(3397) }
                });

            migrationBuilder.InsertData(
                table: "LocationTypesInstances",
                columns: new[] { "Id", "Adress", "CreatedDate", "Description", "LocationTypeID", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Iasi", new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2430), "MAIN Warehouse", 1, new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2431) },
                    { 2, "Iasi", new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2648), "Iasi Mall", 2, new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2649) },
                    { 3, "Suceava", new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2650), "Suceava Mall", 2, new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2651) },
                    { 4, "Client", new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2652), "Goods Assigned to clients", 3, new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2653) },
                    { 5, "Iasi", new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2654), "Returned Items", 1, new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2654) },
                    { 6, "Iasi", new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2656), "Item Supplier", 4, new DateTime(2026, 1, 23, 16, 12, 36, 599, DateTimeKind.Local).AddTicks(2656) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserTypeId",
                table: "Accounts",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsGoodsEntity_AccountId",
                table: "AccountsGoodsEntity",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CashRegisterEntity_LocationID",
                table: "CashRegisterEntity",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_CashRegisterEntitySessions_AssignedClerk",
                table: "CashRegisterEntitySessions",
                column: "AssignedClerk");

            migrationBuilder.CreateIndex(
                name: "IX_CashRegisterEntitySessions_CashRegisterID",
                table: "CashRegisterEntitySessions",
                column: "CashRegisterID");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsTypes_GoodModelId",
                table: "GoodsTypes",
                column: "GoodModelId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsTypesInstances_GoodModelId",
                table: "GoodsTypesInstances",
                column: "GoodModelId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsTypesInstances_LocationId",
                table: "GoodsTypesInstances",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationEntity_LocationType",
                table: "LocationEntity",
                column: "LocationType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationTypesInstances_LocationTypeID",
                table: "LocationTypesInstances",
                column: "LocationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LocationTypesInstancesTasksEntitiesProcurements_LocationTypesInstancesId",
                table: "LocationTypesInstancesTasksEntitiesProcurements",
                column: "LocationTypesInstancesId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportsEntitiesResults_ReportID",
                table: "ReportsEntitiesResults",
                column: "ReportID");

            migrationBuilder.CreateIndex(
                name: "IX_StoreCartsEntityDetails_CartId",
                table: "StoreCartsEntityDetails",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_TasksEntities_userID",
                table: "TasksEntities",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_TasksEntitiesProcurements_GoodTypeID",
                table: "TasksEntitiesProcurements",
                column: "GoodTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TasksEntitiesProcurements_TaskID",
                table: "TasksEntitiesProcurements",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_TasksEntitiesTransfer_GoodID",
                table: "TasksEntitiesTransfer",
                column: "GoodID");

            migrationBuilder.CreateIndex(
                name: "IX_TasksEntitiesTransfer_TaskID",
                table: "TasksEntitiesTransfer",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_TasksEntitiesTransfer_ToLocation",
                table: "TasksEntitiesTransfer",
                column: "ToLocation");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTypeId",
                table: "Users",
                column: "UserTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountsGoodsEntity");

            migrationBuilder.DropTable(
                name: "CashRegisterEntitySessions");

            migrationBuilder.DropTable(
                name: "ItemMovementEntity");

            migrationBuilder.DropTable(
                name: "LocationTypesInstancesTasksEntitiesProcurements");

            migrationBuilder.DropTable(
                name: "ReportsEntitiesResults");

            migrationBuilder.DropTable(
                name: "StoreCartsEntityDetails");

            migrationBuilder.DropTable(
                name: "TasksEntitiesTransfer");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "CashRegisterEntity");

            migrationBuilder.DropTable(
                name: "TasksEntitiesProcurements");

            migrationBuilder.DropTable(
                name: "ReportsEntities");

            migrationBuilder.DropTable(
                name: "StoreCartsEntity");

            migrationBuilder.DropTable(
                name: "GoodsTypesInstances");

            migrationBuilder.DropTable(
                name: "TasksEntities");

            migrationBuilder.DropTable(
                name: "GoodsTypes");

            migrationBuilder.DropTable(
                name: "LocationTypesInstances");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "GoodModelBaseType");

            migrationBuilder.DropTable(
                name: "LocationEntity");

            migrationBuilder.DropTable(
                name: "UserTypes");
        }
    }
}

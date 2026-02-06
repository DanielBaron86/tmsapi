using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasksAPI.Migrations
{
    /// <inheritdoc />
    public partial class ItemInstanceView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"CREATE OR ALTER VIEW v_GoodsTypesInstances AS
                        select i.Id,i.GoodModelId,g.GoodBaseId, i.Price,i.LocationId,l.Description AS LocationName ,i.serialNumber,i.Status,i.Quantity,g.InventoryKey, g.Name,b.Description AS Type,b.Manufacturer,i.CreatedDate,i.UpdatedDate
                        from GoodsTypesInstances i
                        JOIN GoodsTypes g on i.GoodModelId = g.Id
                        JOIN GoodModelBaseType b on  g.GoodBaseId =b.Id
                        JOIN LocationTypesInstances l on i.LocationId =l.Id";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW v_GoodsTypesInstances");
        }
    }
}

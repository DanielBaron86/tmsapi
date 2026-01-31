using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasksAPI.Migrations
{
    /// <inheritdoc />
    public partial class ItemsView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"CREATE OR ALTER VIEW v_GoodsTypesInstances as
                        select g.Id,g.GoodModelId,t.Name,g.Price,g.serialNumber,g.Status, g.LocationId,l.Description,g.CreatedDate,g.UpdatedDate
                        from GoodsTypesInstances g
                        JOIN GoodsTypes t on g.GoodModelId = t.Id
                        JOIN LocationTypesInstances l on g.LocationId=l.Id";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW v_GoodsTypesInstances");
        }
    }
}

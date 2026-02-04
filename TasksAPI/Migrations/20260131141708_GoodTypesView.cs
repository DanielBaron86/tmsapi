using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasksAPI.Migrations
{
    /// <inheritdoc />
    public partial class Views : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"CREATE OR ALTER VIEW v_GoodsTypes AS
                        select g.Id,g.GoodBaseId,g.Name,g.Description,b.Description as Type, b.Manufacturer,g.InventoryKey, g.CreatedDate,g.UpdatedDate
                        from GoodsTypes g
                        JOIN GoodModelBaseType b on g.GoodBaseId = b.Id";
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW v_GoodsTypes");
        }

        
    }
}

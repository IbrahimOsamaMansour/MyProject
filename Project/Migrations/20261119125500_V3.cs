using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "UnitCost",
                table: "product_Quantity",
                type: "float",
                nullable: true);

            migrationBuilder.Sql(@"
                UPDATE stock
                SET stock.UnitCost = source.UnitPrice
                FROM product_Quantity AS stock
                INNER JOIN Product_Purchasing AS link ON link.product_Id = stock.Prod_Id
                CROSS APPLY (
                    SELECT TOP (1) pq.UnitPrice
                    FROM Purchasing_Quantity AS pq
                    WHERE pq.Purch_id = link.Purchasing_Id
                      AND pq.UnitQuantity = stock.UnitQuantity
                    ORDER BY pq.ID
                ) AS source;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitCost",
                table: "product_Quantity");
        }
    }
}

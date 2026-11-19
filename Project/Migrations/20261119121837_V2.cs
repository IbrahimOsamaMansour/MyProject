using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "UnitPrice",
                table: "Purchasing_Quantity",
                type: "float",
                nullable: true);

            // Preserve the old purchase-level price on each existing quantity row.
            migrationBuilder.Sql(@"
                UPDATE pq
                SET pq.UnitPrice = p.UnitPrice
                FROM Purchasing_Quantity AS pq
                INNER JOIN purchasing AS p ON p.ID = pq.Purch_id;");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "purchasing");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "UnitPrice",
                table: "purchasing",
                type: "float",
                nullable: true);

            migrationBuilder.Sql(@"
                UPDATE p
                SET p.UnitPrice = (
                    SELECT TOP (1) pq.UnitPrice
                    FROM Purchasing_Quantity AS pq
                    WHERE pq.Purch_id = p.ID
                    ORDER BY pq.ID
                )
                FROM purchasing AS p;");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "Purchasing_Quantity");

        }
    }
}

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Project.Models;

#nullable disable

namespace Project.Migrations
{
    [DbContext(typeof(ElQasrDbContext))]
    [Migration("20261121124500_AddInvoiceProfileSettings")]
    public partial class AddInvoiceProfileSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceProfileSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DisplayNameEn = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    DisplayNameAr = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    BusinessNameEn = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    BusinessNameAr = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    SubtitleEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    SubtitleAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    AddressEn = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    AddressAr = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceProfileSettings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "InvoiceProfileSettings");
        }
    }
}

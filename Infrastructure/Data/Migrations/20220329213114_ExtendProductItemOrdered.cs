using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class ExtendProductItemOrdered : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemOrdered_ProductName",
                table: "OrderItems",
                newName: "ItemOrdered_ProductNameEn");

            migrationBuilder.AddColumn<string>(
                name: "ItemOrdered_ProductNameAr",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemOrdered_ProductNameAr",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "ItemOrdered_ProductNameEn",
                table: "OrderItems",
                newName: "ItemOrdered_ProductName");
        }
    }
}

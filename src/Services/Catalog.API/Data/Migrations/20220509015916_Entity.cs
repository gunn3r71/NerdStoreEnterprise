using Microsoft.EntityFrameworkCore.Migrations;

namespace NerdStoreEnterprise.Services.Catalog.API.Data.Migrations
{
    public partial class Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TempId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Products_TempId",
                table: "Products",
                column: "TempId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Products_TempId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TempId",
                table: "Products");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NerdStoreEnterprise.Services.Customer.API.Data.CustomersMigrations
{
    public partial class NameRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                    "Customers",
                    table => new
                    {
                        Id = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                        Name = table.Column<string>("VARCHAR(150)", maxLength: 150, nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        Email_Address = table.Column<string>("VARCHAR(254)", maxLength: 254, nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        Cpf_Number = table.Column<string>("VARCHAR(11)", maxLength: 11, nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        Deleted = table.Column<bool>("tinyint(1)", nullable: false, defaultValue: false),
                        TempId1 = table.Column<int>("int", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Customers", x => x.Id);
                        table.UniqueConstraint("AK_Customers_TempId1", x => x.TempId1);
                    })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                    "Adresses",
                    table => new
                    {
                        Id = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                        StreetName = table.Column<string>("VARCHAR(200)", maxLength: 200, nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        BuildingNumber = table.Column<string>("VARCHAR(10)", maxLength: 10, nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        AddressComplement = table.Column<string>("VARCHAR(200)", maxLength: 200, nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        ZipCode = table.Column<string>("varchar(100)", nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        City = table.Column<string>("VARCHAR(100)", maxLength: 100, nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        State = table.Column<string>("VARCHAR(100)", maxLength: 100, nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        CustomerId = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                        TempId = table.Column<int>("int", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Adresses", x => x.Id);
                        table.UniqueConstraint("AK_Adresses_TempId", x => x.TempId);
                        table.ForeignKey(
                            "FK_Adresses_Customers_CustomerId",
                            x => x.CustomerId,
                            "Customers",
                            "Id",
                            onDelete: ReferentialAction.Cascade);
                    })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                "IX_Adresses_CustomerId",
                "Adresses",
                "CustomerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Adresses");

            migrationBuilder.DropTable(
                "Customers");
        }
    }
}
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWeb.Data.Migrations
{
    public partial class ProductImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "productImages",
                columns: table => new
                {
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Product = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageBin = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Alt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productImages", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_productImages_Products_Product",
                        column: x => x.Product,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_productImages_Product",
                table: "productImages",
                column: "Product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productImages");
        }
    }
}

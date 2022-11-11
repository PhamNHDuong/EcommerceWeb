using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWeb.Data.Migrations
{
    public partial class NoManualForeignKeyProductImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productImages_Products_Product",
                table: "productImages");

            migrationBuilder.RenameColumn(
                name: "Product",
                table: "productImages",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_productImages_Product",
                table: "productImages",
                newName: "IX_productImages_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_productImages_Products_ProductId",
                table: "productImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productImages_Products_ProductId",
                table: "productImages");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "productImages",
                newName: "Product");

            migrationBuilder.RenameIndex(
                name: "IX_productImages_ProductId",
                table: "productImages",
                newName: "IX_productImages_Product");

            migrationBuilder.AddForeignKey(
                name: "FK_productImages_Products_Product",
                table: "productImages",
                column: "Product",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

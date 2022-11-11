using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWeb.Data.Migrations
{
    public partial class AddingFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InStock",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AUser",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AUser");

            migrationBuilder.AddColumn<bool>(
                name: "InStock",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWeb.Data.Migrations
{
    public partial class ChangeProductImageDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "productImages");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "productImages",
                newName: "ImageId");

            migrationBuilder.AlterColumn<string>(
                name: "Alt",
                table: "productImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "productImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "productImages");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "productImages",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Alt",
                table: "productImages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "productImages",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

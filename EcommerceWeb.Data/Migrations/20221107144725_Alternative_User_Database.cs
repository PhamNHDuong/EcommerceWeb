using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWeb.Data.Migrations
{
    public partial class Alternative_User_Database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AUserId",
                table: "Ratings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AUser",
                columns: table => new
                {
                    AUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUser", x => x.AUserId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_AUserId",
                table: "Ratings",
                column: "AUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AUser_AUserId",
                table: "Ratings",
                column: "AUserId",
                principalTable: "AUser",
                principalColumn: "AUserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AUser_AUserId",
                table: "Ratings");

            migrationBuilder.DropTable(
                name: "AUser");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_AUserId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "AUserId",
                table: "Ratings");
        }
    }
}

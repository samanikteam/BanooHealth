using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class banner10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar10",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link10",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title10",
                table: "Banners",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar10",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Link10",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Title10",
                table: "Banners");
        }
    }
}

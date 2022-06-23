using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class BannerTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Link1",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Link2",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Link3",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Link4",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Link5",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Link6",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Link7",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Link8",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Link9",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link1",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Link2",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Link3",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Link4",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Link5",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Link6",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Link7",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Link8",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Link9",
                table: "Banners");
        }
    }
}

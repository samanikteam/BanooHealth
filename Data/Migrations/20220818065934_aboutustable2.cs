using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class aboutustable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarAlt",
                table: "Abouts",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AvatarTitle",
                table: "Abouts",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarAlt",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "AvatarTitle",
                table: "Abouts");
        }
    }
}

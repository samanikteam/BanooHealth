using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class popularProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "popular",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "popular",
                table: "Products");
        }
    }
}

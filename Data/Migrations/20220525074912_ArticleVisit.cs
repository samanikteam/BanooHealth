using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ArticleVisit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Visit",
                table: "Articles",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visit",
                table: "Articles");
        }
    }
}

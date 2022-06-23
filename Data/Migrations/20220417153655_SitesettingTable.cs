using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class SitesettingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiteSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LogoAlt = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Favicon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaviconTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FaviconAlt = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Paging = table.Column<int>(type: "int", nullable: false),
                    SMTP = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSettings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteSettings");
        }
    }
}

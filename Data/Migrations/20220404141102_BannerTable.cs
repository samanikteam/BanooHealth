using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class BannerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title1 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Avatar1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Avatar2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title3 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Avatar3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title4 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Avatar4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title5 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Avatar5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title6 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Avatar6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title7 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Avatar7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title8 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Avatar8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title9 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Avatar9 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banners", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banners");
        }
    }
}

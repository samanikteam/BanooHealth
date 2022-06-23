using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addtablefilter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TypeFilterCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SortNum = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeFilterCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilterCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    ProCategoryId = table.Column<int>(type: "int", nullable: true),
                    NameProperty = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TypeFilterCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FilterCategories_ProCategories_ProCategoryId",
                        column: x => x.ProCategoryId,
                        principalTable: "ProCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FilterCategories_TypeFilterCategories_TypeFilterCategoryId",
                        column: x => x.TypeFilterCategoryId,
                        principalTable: "TypeFilterCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilterCategories_CategoryId",
                table: "FilterCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterCategories_ProCategoryId",
                table: "FilterCategories",
                column: "ProCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterCategories_TypeFilterCategoryId",
                table: "FilterCategories",
                column: "TypeFilterCategoryId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilterCategories");

            migrationBuilder.DropTable(
                name: "TypeFilterCategories");
        }
    }
}

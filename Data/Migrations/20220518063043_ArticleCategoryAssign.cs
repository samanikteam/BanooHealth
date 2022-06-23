using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ArticleCategoryAssign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleCategoryAssigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SortNum = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCategoryAssigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleCategoryAssigns_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArticleCategoryAssigns_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleCategoryAssigns_ArticleId",
                table: "ArticleCategoryAssigns",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleCategoryAssigns_CategoryId",
                table: "ArticleCategoryAssigns",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleCategoryAssigns");
        }
    }
}

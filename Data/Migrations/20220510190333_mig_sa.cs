using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class mig_sa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProCategories_ProCategoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "FilterCategories");

            migrationBuilder.DropTable(
                name: "ProCategories");

            migrationBuilder.DropTable(
                name: "TypeFilterCategories");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProCategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProCategoryId",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProCategoryId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConfirmTypeId = table.Column<int>(type: "int", nullable: false),
                    Depth = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Path = table.Column<string>(type: "varchar(50)", nullable: true),
                    PathTitle = table.Column<string>(type: "nvarchar(800)", nullable: true),
                    ProductCatTitle = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    VarietyTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProCategories_ProCategories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ProCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TypeFilterCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SortNum = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
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
                    NameProperty = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProCategoryId = table.Column<int>(type: "int", nullable: true),
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
                name: "IX_Products_ProCategoryId",
                table: "Products",
                column: "ProCategoryId");

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

            migrationBuilder.CreateIndex(
                name: "IX_ProCategories_ParentId",
                table: "ProCategories",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProCategories_ProCategoryId",
                table: "Products",
                column: "ProCategoryId",
                principalTable: "ProCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

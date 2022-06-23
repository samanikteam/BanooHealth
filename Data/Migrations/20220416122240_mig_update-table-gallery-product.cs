using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class mig_updatetablegalleryproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductGalleries");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProGalleries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProGalleries_ProductId",
                table: "ProGalleries",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProGalleries_Products_ProductId",
                table: "ProGalleries",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProGalleries_Products_ProductId",
                table: "ProGalleries");

            migrationBuilder.DropIndex(
                name: "IX_ProGalleries_ProductId",
                table: "ProGalleries");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProGalleries");

            migrationBuilder.CreateTable(
                name: "ProductGalleries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProGalleryId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    sortNum = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGalleries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductGalleries_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductGalleries_ProGalleries_ProGalleryId",
                        column: x => x.ProGalleryId,
                        principalTable: "ProGalleries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductGalleries_ProductId",
                table: "ProductGalleries",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGalleries_ProGalleryId",
                table: "ProductGalleries",
                column: "ProGalleryId");
        }
    }
}

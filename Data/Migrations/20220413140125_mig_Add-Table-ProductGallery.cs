using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class mig_AddTableProductGallery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar2",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Avatar3",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Avatar4",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AvatarAlt2",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AvatarAlt3",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AvatarAlt4",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AvatarTitle2",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AvatarTitle3",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AvatarTitle4",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "ProductGalleries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProGalleryId = table.Column<long>(type: "bigint", nullable: false),
                    sortNum = table.Column<int>(type: "int", nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductGalleries");

            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar2",
                table: "Products",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar3",
                table: "Products",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar4",
                table: "Products",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarAlt2",
                table: "Products",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarAlt3",
                table: "Products",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarAlt4",
                table: "Products",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarTitle2",
                table: "Products",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarTitle3",
                table: "Products",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarTitle4",
                table: "Products",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}

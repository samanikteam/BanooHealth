using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class add3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductPictures");

            migrationBuilder.RenameColumn(
                name: "AvatarTitle",
                table: "Products",
                newName: "AvatarTitle1");

            migrationBuilder.RenameColumn(
                name: "AvatarAlt",
                table: "Products",
                newName: "AvatarAlt1");

            migrationBuilder.RenameColumn(
                name: "Avatar",
                table: "Products",
                newName: "Avatar1");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "AvatarTitle1",
                table: "Products",
                newName: "AvatarTitle");

            migrationBuilder.RenameColumn(
                name: "AvatarAlt1",
                table: "Products",
                newName: "AvatarAlt");

            migrationBuilder.RenameColumn(
                name: "Avatar1",
                table: "Products",
                newName: "Avatar");

            migrationBuilder.CreateTable(
                name: "ProductPictures",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Avatar = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    AvatarAlt = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AvatarTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPictures_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPictures_ProductId",
                table: "ProductPictures",
                column: "ProductId");
        }
    }
}

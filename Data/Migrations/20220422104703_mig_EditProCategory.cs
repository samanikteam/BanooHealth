using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class mig_EditProCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "ProCategories");

            migrationBuilder.DropColumn(
                name: "AvatarAlt",
                table: "ProCategories");

            migrationBuilder.DropColumn(
                name: "AvatarTitle",
                table: "ProCategories");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProCategories");

            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "ProCategories");

            migrationBuilder.DropColumn(
                name: "LastUpdateUserId",
                table: "ProCategories");

            migrationBuilder.DropColumn(
                name: "RegisterDate",
                table: "ProCategories");

            migrationBuilder.DropColumn(
                name: "RegisterUserId",
                table: "ProCategories");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "ProCategories");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ProCategories");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                table: "ProCategories",
                newName: "IsDeleted");

            migrationBuilder.AddColumn<int>(
                name: "ConfirmTypeId",
                table: "ProCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Depth",
                table: "ProCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "ProCategories",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathTitle",
                table: "ProCategories",
                type: "nvarchar(800)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductCatTitle",
                table: "ProCategories",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VarietyTypeId",
                table: "ProCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmTypeId",
                table: "ProCategories");

            migrationBuilder.DropColumn(
                name: "Depth",
                table: "ProCategories");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "ProCategories");

            migrationBuilder.DropColumn(
                name: "PathTitle",
                table: "ProCategories");

            migrationBuilder.DropColumn(
                name: "ProductCatTitle",
                table: "ProCategories");

            migrationBuilder.DropColumn(
                name: "VarietyTypeId",
                table: "ProCategories");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "ProCategories",
                newName: "IsDelete");

            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar",
                table: "ProCategories",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarAlt",
                table: "ProCategories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarTitle",
                table: "ProCategories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProCategories",
                type: "nvarchar(155)",
                maxLength: 155,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "ProCategories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdateUserId",
                table: "ProCategories",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterDate",
                table: "ProCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RegisterUserId",
                table: "ProCategories",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "ProCategories",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ProCategories",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}

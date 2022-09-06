using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class mig_x1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "ProComments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ParentId",
                table: "Products",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProComments_ParentId",
                table: "Products",
                column: "ParentId",
                principalTable: "ProComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProComments_ParentId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ParentId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "ProComments");
        }
    }
}

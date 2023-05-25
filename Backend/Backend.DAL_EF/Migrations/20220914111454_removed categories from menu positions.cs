using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.DAL_EF.Migrations
{
    public partial class removedcategoriesfrommenupositions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuPositions_Categories_CategoryId",
                table: "MenuPositions");

            migrationBuilder.DropIndex(
                name: "IX_MenuPositions_CategoryId",
                table: "MenuPositions");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "MenuPositions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "MenuPositions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MenuPositions_CategoryId",
                table: "MenuPositions",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuPositions_Categories_CategoryId",
                table: "MenuPositions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

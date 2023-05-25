using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.DAL_EF.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_CategoryPositions_ServicePoints_ServicePointId",
                table: "CategoryPositions",
                column: "ServicePointId",
                principalTable: "ServicePoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryPositions_ServicePoints_ServicePointId",
                table: "CategoryPositions");
        }
    }
}

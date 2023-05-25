using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.DAL_EF.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServicePointId",
                table: "CategoryPositions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryPositions_ServicePointId",
                table: "CategoryPositions",
                column: "ServicePointId");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_CategoryPositions_ServicePoints_ServicePointId",
            //     table: "CategoryPositions",
            //     column: "ServicePointId",
            //     principalTable: "ServicePoints",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropForeignKey(
            //     name: "FK_CategoryPositions_ServicePoints_ServicePointId",
            //     table: "CategoryPositions");

            migrationBuilder.DropIndex(
                name: "IX_CategoryPositions_ServicePointId",
                table: "CategoryPositions");

            migrationBuilder.DropColumn(
                name: "ServicePointId",
                table: "CategoryPositions");
        }
    }
}

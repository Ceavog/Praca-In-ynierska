using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.DAL_EF.Migrations
{
    public partial class changedrestofmenuPositionsreferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderPositions_Products_MenuPositionId",
                table: "OrderPositions");

            migrationBuilder.RenameColumn(
                name: "MenuPositionId",
                table: "OrderPositions",
                newName: "ProductsId");

            migrationBuilder.RenameColumn(
                name: "ManuPositionId",
                table: "OrderPositions",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderPositions_MenuPositionId",
                table: "OrderPositions",
                newName: "IX_OrderPositions_ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPositions_Products_ProductsId",
                table: "OrderPositions",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderPositions_Products_ProductsId",
                table: "OrderPositions");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "OrderPositions",
                newName: "MenuPositionId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderPositions",
                newName: "ManuPositionId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderPositions_ProductsId",
                table: "OrderPositions",
                newName: "IX_OrderPositions_MenuPositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPositions_Products_MenuPositionId",
                table: "OrderPositions",
                column: "MenuPositionId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

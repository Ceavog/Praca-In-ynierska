using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.DAL_EF.Migrations
{
    public partial class addeduserIdtomenuPositions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "MyProperty",
                table: "Orders",
                newName: "OrderTime");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "MenuPosition",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MenuPosition_UserId",
                table: "MenuPosition",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuPosition_Users_UserId",
                table: "MenuPosition",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuPosition_Users_UserId",
                table: "MenuPosition");

            migrationBuilder.DropIndex(
                name: "IX_MenuPosition_UserId",
                table: "MenuPosition");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MenuPosition");

            migrationBuilder.RenameColumn(
                name: "OrderTime",
                table: "Orders",
                newName: "MyProperty");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Orders",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}

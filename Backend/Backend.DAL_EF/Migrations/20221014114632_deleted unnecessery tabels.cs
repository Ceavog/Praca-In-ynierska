using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.DAL_EF.Migrations
{
    public partial class deletedunnecesserytabels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuPositions_ServicePoints_ServicePointId",
                table: "MenuPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPositions_MenuPositions_MenuPositionId",
                table: "OrderPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Employees_EmployeeId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ServicePoints_ServicePointId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_ServicePoints_ServicePointId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "CategoryPositions");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "ServicePoints");

            migrationBuilder.DropIndex(
                name: "IX_Users_ServicePointId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Orders_EmployeeId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ServicePointId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuPositions",
                table: "MenuPositions");

            migrationBuilder.DropIndex(
                name: "IX_MenuPositions_ServicePointId",
                table: "MenuPositions");

            migrationBuilder.DropColumn(
                name: "ServicePointId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ServicePointId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ServicePointId",
                table: "MenuPositions");

            migrationBuilder.RenameTable(
                name: "MenuPositions",
                newName: "MenuPosition");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuPosition",
                table: "MenuPosition",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPositions_MenuPosition_MenuPositionId",
                table: "OrderPositions",
                column: "MenuPositionId",
                principalTable: "MenuPosition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderPositions_MenuPosition_MenuPositionId",
                table: "OrderPositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuPosition",
                table: "MenuPosition");

            migrationBuilder.RenameTable(
                name: "MenuPosition",
                newName: "MenuPositions");

            migrationBuilder.AddColumn<int>(
                name: "ServicePointId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServicePointId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServicePointId",
                table: "MenuPositions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuPositions",
                table: "MenuPositions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ServicePoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePoints", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ServicePointId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_ServicePoints_ServicePointId",
                        column: x => x.ServicePointId,
                        principalTable: "ServicePoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ServicePointId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_ServicePoints_ServicePointId",
                        column: x => x.ServicePointId,
                        principalTable: "ServicePoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ServicePointId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecondName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_ServicePoints_ServicePointId",
                        column: x => x.ServicePointId,
                        principalTable: "ServicePoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CategoryPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    MenuPositionId = table.Column<int>(type: "int", nullable: false),
                    ServicePointId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryPositions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryPositions_MenuPositions_MenuPositionId",
                        column: x => x.MenuPositionId,
                        principalTable: "MenuPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryPositions_ServicePoints_ServicePointId",
                        column: x => x.ServicePointId,
                        principalTable: "ServicePoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliveries_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deliveries_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ServicePointId",
                table: "Users",
                column: "ServicePointId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EmployeeId",
                table: "Orders",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ServicePointId",
                table: "Orders",
                column: "ServicePointId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuPositions_ServicePointId",
                table: "MenuPositions",
                column: "ServicePointId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ServicePointId",
                table: "Categories",
                column: "ServicePointId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryPositions_CategoryId",
                table: "CategoryPositions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryPositions_MenuPositionId",
                table: "CategoryPositions",
                column: "MenuPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryPositions_ServicePointId",
                table: "CategoryPositions",
                column: "ServicePointId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ServicePointId",
                table: "Clients",
                column: "ServicePointId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_ClientId",
                table: "Deliveries",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_OrderId",
                table: "Deliveries",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ServicePointId",
                table: "Employees",
                column: "ServicePointId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuPositions_ServicePoints_ServicePointId",
                table: "MenuPositions",
                column: "ServicePointId",
                principalTable: "ServicePoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPositions_MenuPositions_MenuPositionId",
                table: "OrderPositions",
                column: "MenuPositionId",
                principalTable: "MenuPositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Employees_EmployeeId",
                table: "Orders",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ServicePoints_ServicePointId",
                table: "Orders",
                column: "ServicePointId",
                principalTable: "ServicePoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ServicePoints_ServicePointId",
                table: "Users",
                column: "ServicePointId",
                principalTable: "ServicePoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

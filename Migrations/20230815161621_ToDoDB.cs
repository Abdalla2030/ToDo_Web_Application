using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDo_Web_App.Migrations
{
    public partial class ToDoDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    categoryID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    categoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.categoryID);
                });

            migrationBuilder.CreateTable(
                name: "statuses",
                columns: table => new
                {
                    statusID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    statusName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statuses", x => x.statusID);
                });

            migrationBuilder.CreateTable(
                name: "ToDos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    categoryID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    statusID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ToDos_categories_categoryID",
                        column: x => x.categoryID,
                        principalTable: "categories",
                        principalColumn: "categoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToDos_statuses_statusID",
                        column: x => x.statusID,
                        principalTable: "statuses",
                        principalColumn: "statusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "categoryID", "categoryName" },
                values: new object[,]
                {
                    { "work", "Work" },
                    { "home", "Home" },
                    { "shop", "Shopping" },
                    { "call", "Contact" },
                    { "ex", "Exercise" }
                });

            migrationBuilder.InsertData(
                table: "statuses",
                columns: new[] { "statusID", "statusName" },
                values: new object[,]
                {
                    { "open", "Open" },
                    { "closed", "Closed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_categoryID",
                table: "ToDos",
                column: "categoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_statusID",
                table: "ToDos",
                column: "statusID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDos");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "statuses");
        }
    }
}

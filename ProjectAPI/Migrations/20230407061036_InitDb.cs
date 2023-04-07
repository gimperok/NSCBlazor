using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Cargo = table.Column<string>(type: "TEXT", nullable: true),
                    Tel = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateCreate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateModify = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: true),
                    ClientDbId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLists_Clients_ClientDbId",
                        column: x => x.ClientDbId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderStrings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Kod = table.Column<string>(type: "TEXT", nullable: false),
                    Leather = table.Column<string>(type: "TEXT", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: true),
                    Size35 = table.Column<int>(type: "INTEGER", nullable: false),
                    Size36 = table.Column<int>(type: "INTEGER", nullable: false),
                    Size37 = table.Column<int>(type: "INTEGER", nullable: false),
                    Size38 = table.Column<int>(type: "INTEGER", nullable: false),
                    Size39 = table.Column<int>(type: "INTEGER", nullable: false),
                    Size40 = table.Column<int>(type: "INTEGER", nullable: false),
                    Size41 = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    OrderListId = table.Column<int>(type: "INTEGER", nullable: true),
                    OrderListDbId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStrings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderStrings_OrderLists_OrderListDbId",
                        column: x => x.OrderListDbId,
                        principalTable: "OrderLists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderLists_ClientDbId",
                table: "OrderLists",
                column: "ClientDbId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStrings_OrderListDbId",
                table: "OrderStrings",
                column: "OrderListDbId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderStrings");

            migrationBuilder.DropTable(
                name: "OrderLists");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}

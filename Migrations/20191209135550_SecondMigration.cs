using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PieShop.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PieGiftOrders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PieGiftOrders",
                columns: table => new
                {
                    PieGiftOrderId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PieId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PieGiftOrders", x => x.PieGiftOrderId);
                    table.ForeignKey(
                        name: "FK_PieGiftOrders_Pies_PieId",
                        column: x => x.PieId,
                        principalTable: "Pies",
                        principalColumn: "PieId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PieGiftOrders_PieId",
                table: "PieGiftOrders",
                column: "PieId");
        }
    }
}

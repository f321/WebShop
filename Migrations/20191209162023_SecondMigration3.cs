using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PieShop.Migrations
{
    public partial class SecondMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeInformation");

            /*migrationBuilder.DropColumn(
                name: "AllergyInformation",
                table: "Pies");

            migrationBuilder.DropColumn(
                name: "ImageThumbnailUrl",
                table: "Pies");*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AllergyInformation",
                table: "Pies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageThumbnailUrl",
                table: "Pies",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RecipeInformation",
                columns: table => new
                {
                    RecipeInformationId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Duration = table.Column<int>(nullable: false),
                    PieId = table.Column<int>(nullable: false),
                    PreparationDirections = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeInformation", x => x.RecipeInformationId);
                    table.ForeignKey(
                        name: "FK_RecipeInformation_Pies_PieId",
                        column: x => x.PieId,
                        principalTable: "Pies",
                        principalColumn: "PieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeInformation_PieId",
                table: "RecipeInformation",
                column: "PieId",
                unique: true);
        }
    }
}

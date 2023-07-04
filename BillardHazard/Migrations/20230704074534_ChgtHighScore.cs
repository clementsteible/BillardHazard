using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillardHazard.Migrations
{
    /// <inheritdoc />
    public partial class ChgtHighScore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HighScore_Team_TeamId",
                table: "HighScore");

            migrationBuilder.DropIndex(
                name: "IX_HighScore_TeamId",
                table: "HighScore");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "HighScore");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "HighScore",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TeamName",
                table: "HighScore",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "HighScore");

            migrationBuilder.DropColumn(
                name: "TeamName",
                table: "HighScore");

            migrationBuilder.AddColumn<Guid>(
                name: "TeamId",
                table: "HighScore",
                type: "char(36)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HighScore_TeamId",
                table: "HighScore",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_HighScore_Team_TeamId",
                table: "HighScore",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id");
        }
    }
}

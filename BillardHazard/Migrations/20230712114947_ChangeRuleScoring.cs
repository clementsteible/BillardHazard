using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillardHazard.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRuleScoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "Rule",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "Rule");
        }
    }
}

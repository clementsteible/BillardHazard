using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillardHazard.Migrations
{
    /// <inheritdoc />
    public partial class ChgtTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsItsTurn",
                table: "Team",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsItsTurn",
                table: "Team");
        }
    }
}

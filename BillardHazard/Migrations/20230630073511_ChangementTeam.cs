using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillardHazard.Migrations
{
    /// <inheritdoc />
    public partial class ChangementTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Team",
                type: "longtext",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Team");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolisProReminder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addvehiclesdependency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByAgentId",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByAgentId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Vehicles");
        }
    }
}

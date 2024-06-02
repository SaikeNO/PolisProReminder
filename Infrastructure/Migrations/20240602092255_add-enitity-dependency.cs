using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolisProReminder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addenititydependency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByAgentId",
                table: "Insurers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Insurers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByAgentId",
                table: "InsuranceTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "InsuranceTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByAgentId",
                table: "InsuranceCompanies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "InsuranceCompanies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByAgentId",
                table: "Insurers");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Insurers");

            migrationBuilder.DropColumn(
                name: "CreatedByAgentId",
                table: "InsuranceTypes");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "InsuranceTypes");

            migrationBuilder.DropColumn(
                name: "CreatedByAgentId",
                table: "InsuranceCompanies");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "InsuranceCompanies");
        }
    }
}

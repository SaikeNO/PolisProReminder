using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolisProReminder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addNoteToPolicy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Policies",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Policies");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolisProReminder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addagentidtopolicy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_AspNetUsers_CreatedById",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Policies_CreatedById",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Policies");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByAgentId",
                table: "Policies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Policies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByAgentId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Policies");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Policies",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_CreatedById",
                table: "Policies",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_AspNetUsers_CreatedById",
                table: "Policies",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

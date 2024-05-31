using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolisProReminder.Migrations
{
    public partial class addCreatedById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SuperiorId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Policies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_SuperiorId",
                table: "Users",
                column: "SuperiorId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_CreatedById",
                table: "Policies",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Users_CreatedById",
                table: "Policies",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_SuperiorId",
                table: "Users",
                column: "SuperiorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Users_CreatedById",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_SuperiorId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_SuperiorId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Policies_CreatedById",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "SuperiorId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Policies");
        }
    }
}

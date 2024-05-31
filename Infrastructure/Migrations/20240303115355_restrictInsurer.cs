using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolisProReminder.Migrations
{
    public partial class restrictInsurer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Insurers_InsurerId",
                table: "Policies");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Insurers_InsurerId",
                table: "Policies",
                column: "InsurerId",
                principalTable: "Insurers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Insurers_InsurerId",
                table: "Policies");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Insurers_InsurerId",
                table: "Policies",
                column: "InsurerId",
                principalTable: "Insurers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

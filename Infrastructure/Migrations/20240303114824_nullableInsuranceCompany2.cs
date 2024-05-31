using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolisProReminder.Migrations
{
    public partial class nullableInsuranceCompany2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_InsuranceCompanies_InsuranceCompanyId",
                table: "Policies");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_InsuranceCompanies_InsuranceCompanyId",
                table: "Policies",
                column: "InsuranceCompanyId",
                principalTable: "InsuranceCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_InsuranceCompanies_InsuranceCompanyId",
                table: "Policies");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_InsuranceCompanies_InsuranceCompanyId",
                table: "Policies",
                column: "InsuranceCompanyId",
                principalTable: "InsuranceCompanies",
                principalColumn: "Id");
        }
    }
}

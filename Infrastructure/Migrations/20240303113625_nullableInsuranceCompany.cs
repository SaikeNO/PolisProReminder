using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolisProReminder.Migrations
{
    public partial class nullableInsuranceCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_InsuranceCompanies_InsuranceCompanyId",
                table: "Policies");

            migrationBuilder.AlterColumn<int>(
                name: "InsuranceCompanyId",
                table: "Policies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_InsuranceCompanies_InsuranceCompanyId",
                table: "Policies",
                column: "InsuranceCompanyId",
                principalTable: "InsuranceCompanies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_InsuranceCompanies_InsuranceCompanyId",
                table: "Policies");

            migrationBuilder.AlterColumn<int>(
                name: "InsuranceCompanyId",
                table: "Policies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_InsuranceCompanies_InsuranceCompanyId",
                table: "Policies",
                column: "InsuranceCompanyId",
                principalTable: "InsuranceCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

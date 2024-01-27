using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolisProReminder.Migrations
{
    public partial class changenames2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceTypePolicy_Policies_InsurancePoliciesId",
                table: "InsuranceTypePolicy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InsuranceTypePolicy",
                table: "InsuranceTypePolicy");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceTypePolicy_InsuranceTypesId",
                table: "InsuranceTypePolicy");

            migrationBuilder.RenameColumn(
                name: "InsurancePoliciesId",
                table: "InsuranceTypePolicy",
                newName: "PoliciesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InsuranceTypePolicy",
                table: "InsuranceTypePolicy",
                columns: new[] { "InsuranceTypesId", "PoliciesId" });

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceTypePolicy_PoliciesId",
                table: "InsuranceTypePolicy",
                column: "PoliciesId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceTypePolicy_Policies_PoliciesId",
                table: "InsuranceTypePolicy",
                column: "PoliciesId",
                principalTable: "Policies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceTypePolicy_Policies_PoliciesId",
                table: "InsuranceTypePolicy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InsuranceTypePolicy",
                table: "InsuranceTypePolicy");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceTypePolicy_PoliciesId",
                table: "InsuranceTypePolicy");

            migrationBuilder.RenameColumn(
                name: "PoliciesId",
                table: "InsuranceTypePolicy",
                newName: "InsurancePoliciesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InsuranceTypePolicy",
                table: "InsuranceTypePolicy",
                columns: new[] { "InsurancePoliciesId", "InsuranceTypesId" });

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceTypePolicy_InsuranceTypesId",
                table: "InsuranceTypePolicy",
                column: "InsuranceTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceTypePolicy_Policies_InsurancePoliciesId",
                table: "InsuranceTypePolicy",
                column: "InsurancePoliciesId",
                principalTable: "Policies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolisProReminder.Migrations
{
    public partial class changenames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsurancePolicyInsuranceType");

            migrationBuilder.CreateTable(
                name: "InsuranceTypePolicy",
                columns: table => new
                {
                    InsurancePoliciesId = table.Column<int>(type: "int", nullable: false),
                    InsuranceTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceTypePolicy", x => new { x.InsurancePoliciesId, x.InsuranceTypesId });
                    table.ForeignKey(
                        name: "FK_InsuranceTypePolicy_InsuranceTypes_InsuranceTypesId",
                        column: x => x.InsuranceTypesId,
                        principalTable: "InsuranceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsuranceTypePolicy_Policies_InsurancePoliciesId",
                        column: x => x.InsurancePoliciesId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceTypePolicy_InsuranceTypesId",
                table: "InsuranceTypePolicy",
                column: "InsuranceTypesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsuranceTypePolicy");

            migrationBuilder.CreateTable(
                name: "InsurancePolicyInsuranceType",
                columns: table => new
                {
                    InsurancePoliciesId = table.Column<int>(type: "int", nullable: false),
                    InsuranceTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurancePolicyInsuranceType", x => new { x.InsurancePoliciesId, x.InsuranceTypesId });
                    table.ForeignKey(
                        name: "FK_InsurancePolicyInsuranceType_InsuranceTypes_InsuranceTypesId",
                        column: x => x.InsuranceTypesId,
                        principalTable: "InsuranceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsurancePolicyInsuranceType_Policies_InsurancePoliciesId",
                        column: x => x.InsurancePoliciesId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicyInsuranceType_InsuranceTypesId",
                table: "InsurancePolicyInsuranceType",
                column: "InsuranceTypesId");
        }
    }
}

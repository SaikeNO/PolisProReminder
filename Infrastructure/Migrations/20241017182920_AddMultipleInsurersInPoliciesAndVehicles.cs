using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolisProReminder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMultipleInsurersInPoliciesAndVehicles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Insurers_InsurerId",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Insurers_InsurerId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_InsurerId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Policies_InsurerId",
                table: "Policies");

            migrationBuilder.CreateTable(
                name: "InsurerPolicies",
                columns: table => new
                {
                    PolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsurerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurerPolicies", x => new { x.PolicyId, x.InsurerId });
                    table.ForeignKey(
                        name: "FK_InsurerPolicies_Insurers_InsurerId",
                        column: x => x.InsurerId,
                        principalTable: "Insurers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsurerPolicies_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InsurerVehicles",
                columns: table => new
                {
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsurerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurerVehicles", x => new { x.VehicleId, x.InsurerId });
                    table.ForeignKey(
                        name: "FK_InsurerVehicles_Insurers_InsurerId",
                        column: x => x.InsurerId,
                        principalTable: "Insurers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsurerVehicles_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InsurerPolicies_InsurerId",
                table: "InsurerPolicies",
                column: "InsurerId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurerVehicles_InsurerId",
                table: "InsurerVehicles",
                column: "InsurerId");

            migrationBuilder.Sql("INSERT INTO InsurerPolicies (PolicyId, InsurerId) SELECT Id AS PolicyId, InsurerId AS InsurerId FROM Policies WHERE InsurerId IS NOT NULL;");
            migrationBuilder.Sql("INSERT INTO InsurerVehicles (VehicleId, InsurerId) SELECT Id AS VehicleId, InsurerId AS InsurerId FROM Vehicles WHERE InsurerId IS NOT NULL;");

            migrationBuilder.DropColumn(
                name: "InsurerId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "InsurerId",
                table: "Policies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsurerPolicies");

            migrationBuilder.DropTable(
                name: "InsurerVehicles");

            migrationBuilder.AddColumn<Guid>(
                name: "InsurerId",
                table: "Vehicles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "InsurerId",
                table: "Policies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_InsurerId",
                table: "Vehicles",
                column: "InsurerId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_InsurerId",
                table: "Policies",
                column: "InsurerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Insurers_InsurerId",
                table: "Policies",
                column: "InsurerId",
                principalTable: "Insurers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Insurers_InsurerId",
                table: "Vehicles",
                column: "InsurerId",
                principalTable: "Insurers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

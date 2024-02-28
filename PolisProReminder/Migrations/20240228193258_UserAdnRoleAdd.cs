using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolisProReminder.Migrations
{
    public partial class UserAdnRoleAdd : Migration
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

            migrationBuilder.AlterColumn<string>(
                name: "PolicyNumber",
                table: "Policies",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Pesel",
                table: "Insurers",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InsuranceTypePolicy",
                table: "InsuranceTypePolicy",
                columns: new[] { "InsuranceTypesId", "PoliciesId" });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceTypePolicy_PoliciesId",
                table: "InsuranceTypePolicy",
                column: "PoliciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

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

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

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

            migrationBuilder.AlterColumn<string>(
                name: "PolicyNumber",
                table: "Policies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "Pesel",
                table: "Insurers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

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

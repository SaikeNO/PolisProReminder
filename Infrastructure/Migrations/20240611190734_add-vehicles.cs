using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolisProReminder.Infrastructure.Migrations
{
    /// <inheritdoc />
#pragma warning disable CS8981 // Nazwa typu zawiera tylko małe litery ascii. Takie nazwy mogą zostać zarezerwowane dla języka.
    public partial class addvehicles : Migration
#pragma warning restore CS8981 // Nazwa typu zawiera tylko małe litery ascii. Takie nazwy mogą zostać zarezerwowane dla języka.
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VehicleId",
                table: "Policies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    FirstRegistrationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    VIN = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: true),
                    InsurerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Insurers_InsurerId",
                        column: x => x.InsurerId,
                        principalTable: "Insurers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Policies_VehicleId",
                table: "Policies",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_InsurerId",
                table: "Vehicles",
                column: "InsurerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Vehicles_VehicleId",
                table: "Policies",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Vehicles_VehicleId",
                table: "Policies");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Policies_VehicleId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Policies");
        }
    }
}

﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolisProReminder.Infrastructure.Migrations
{
    /// <inheritdoc />
#pragma warning disable CS8981 // Nazwa typu zawiera tylko małe litery ascii. Takie nazwy mogą zostać zarezerwowane dla języka.
    public partial class userdependency : Migration
#pragma warning restore CS8981 // Nazwa typu zawiera tylko małe litery ascii. Takie nazwy mogą zostać zarezerwowane dla języka.
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_SuperiorId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SuperiorId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SuperiorId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AgentId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "SuperiorId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SuperiorId",
                table: "AspNetUsers",
                column: "SuperiorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_SuperiorId",
                table: "AspNetUsers",
                column: "SuperiorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class enditcard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LastSyncIsOk",
                table: "BankCards",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSyncTime",
                table: "BankCards",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OverdueFine",
                table: "BankCards",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastSyncIsOk",
                table: "BankCards");

            migrationBuilder.DropColumn(
                name: "LastSyncTime",
                table: "BankCards");

            migrationBuilder.DropColumn(
                name: "OverdueFine",
                table: "BankCards");
        }
    }
}

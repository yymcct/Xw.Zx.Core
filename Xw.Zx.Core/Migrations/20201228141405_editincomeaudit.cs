using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class editincomeaudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuditMemberId",
                table: "IncomeAccounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Auditime",
                table: "IncomeAccounts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "IncomeAccountState",
                table: "IncomeAccounts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuditMemberId",
                table: "IncomeAccounts");

            migrationBuilder.DropColumn(
                name: "Auditime",
                table: "IncomeAccounts");

            migrationBuilder.DropColumn(
                name: "IncomeAccountState",
                table: "IncomeAccounts");
        }
    }
}

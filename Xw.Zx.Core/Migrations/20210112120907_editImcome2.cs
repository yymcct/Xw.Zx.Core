using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class editImcome2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTime",
                table: "Members",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 12, 20, 9, 7, 253, DateTimeKind.Local).AddTicks(3200),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 1, 12, 17, 29, 27, 7, DateTimeKind.Local).AddTicks(5491));

            migrationBuilder.AddColumn<int>(
                name: "WithdrawDepositId",
                table: "IncomeAccounts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WithdrawDepositId",
                table: "IncomeAccounts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTime",
                table: "Members",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 12, 17, 29, 27, 7, DateTimeKind.Local).AddTicks(5491),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 1, 12, 20, 9, 7, 253, DateTimeKind.Local).AddTicks(3200));
        }
    }
}

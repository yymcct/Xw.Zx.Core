using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class addDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "WithdrawDeposits",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "VoiceNews",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "UpdateVipAuthCodes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "SysParams",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Receivables",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Products",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Payments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Members",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "MailSrcs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Mailconfigs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "LxComputers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "IncomeAccounts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "BankCards",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "BankBills",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "BankBillDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "AppVersions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "ApplyForZxs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "AlipayLogs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "WithdrawDeposits");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "VoiceNews");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "UpdateVipAuthCodes");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "SysParams");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Receivables");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "MailSrcs");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Mailconfigs");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "LxComputers");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "IncomeAccounts");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "BankCards");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "BankBills");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "BankBillDetails");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "AppVersions");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "ApplyForZxs");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "AlipayLogs");
        }
    }
}

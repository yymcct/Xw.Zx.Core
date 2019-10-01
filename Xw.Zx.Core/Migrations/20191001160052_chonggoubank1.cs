using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class chonggoubank1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankCardType",
                table: "BankBillDetails");

            migrationBuilder.AddColumn<int>(
                name: "Bank",
                table: "BankBillDetails",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bank",
                table: "BankBillDetails");

            migrationBuilder.AddColumn<int>(
                name: "BankCardType",
                table: "BankBillDetails",
                nullable: false,
                defaultValue: 0);
        }
    }
}

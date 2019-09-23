using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class addbankdails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrased",
                table: "MailSrcs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "MailSrcs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BankBillDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BankCardId = table.Column<string>(nullable: true),
                    TreadTime = table.Column<DateTime>(nullable: false),
                    SellerName = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    Unit = table.Column<string>(nullable: true),
                    MemberID = table.Column<int>(nullable: false),
                    MailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankBillDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankBillDetails");

            migrationBuilder.DropColumn(
                name: "IsPrased",
                table: "MailSrcs");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "MailSrcs");
        }
    }
}

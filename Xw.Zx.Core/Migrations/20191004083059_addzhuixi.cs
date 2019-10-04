using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class addzhuixi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CityCode",
                table: "Members",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplyForZxs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    ApplyForZxState = table.Column<int>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyForZxs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncomeAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    SourceOrderId = table.Column<int>(nullable: false),
                    SourceOrderMemberId = table.Column<int>(nullable: false),
                    SourceOrderMemberInviteId = table.Column<int>(nullable: false),
                    IncomeAccountType = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    AddTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeAccounts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplyForZxs");

            migrationBuilder.DropTable(
                name: "IncomeAccounts");

            migrationBuilder.DropColumn(
                name: "CityCode",
                table: "Members");
        }
    }
}

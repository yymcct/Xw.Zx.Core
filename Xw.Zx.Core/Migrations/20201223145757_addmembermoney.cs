using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class addmembermoney : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Money",
                table: "Members",
                type: "decimal(8, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "MemberBalanceLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Memberid = table.Column<int>(nullable: false),
                    memberMoneySource = table.Column<int>(nullable: false),
                    SourceId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    OriginalMoney = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    CurMoney = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberBalanceLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberBalanceLogs");

            migrationBuilder.DropColumn(
                name: "Money",
                table: "Members");
        }
    }
}

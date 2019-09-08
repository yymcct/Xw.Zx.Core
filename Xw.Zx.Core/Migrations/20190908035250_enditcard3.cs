using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class enditcard3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankBills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BankCardId = table.Column<int>(nullable: false),
                    CycleStart = table.Column<DateTime>(nullable: false),
                    CycleStop = table.Column<DateTime>(nullable: false),
                    Limit = table.Column<decimal>(nullable: false),
                    NewBalance = table.Column<decimal>(nullable: false),
                    PaymentDueData = table.Column<DateTime>(nullable: false),
                    OverdueFine = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankBills", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankBills");
        }
    }
}

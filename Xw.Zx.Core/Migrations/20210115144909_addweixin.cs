using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class addweixin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTime",
                table: "Members",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 15, 22, 49, 7, 966, DateTimeKind.Local).AddTicks(1931),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 1, 12, 20, 9, 7, 253, DateTimeKind.Local).AddTicks(3200));

            migrationBuilder.CreateTable(
                name: "WechatOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    TransactionID = table.Column<string>(type: "varchar(50)", nullable: true),
                    Out_Order_No = table.Column<string>(type: "varchar(50)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TranTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    PayState = table.Column<string>(type: "varchar(50)", nullable: true),
                    SubState = table.Column<string>(type: "varchar(50)", nullable: true),
                    PayDescription = table.Column<string>(type: "varchar(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WechatOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WechatSubDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    TransactionID = table.Column<string>(type: "varchar(50)", nullable: true),
                    Last_Out_Order_No = table.Column<string>(type: "varchar(50)", nullable: true),
                    Return_OrderID = table.Column<string>(type: "varchar(50)", nullable: true),
                    SubType = table.Column<string>(type: "varchar(50)", nullable: true),
                    SubAccount = table.Column<string>(type: "varchar(50)", nullable: true),
                    SubName = table.Column<string>(type: "varchar(50)", nullable: true),
                    SubAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    SubState = table.Column<string>(type: "varchar(50)", nullable: true),
                    PayDescription = table.Column<string>(type: "varchar(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WechatSubDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WechatSubLedgerReceivers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    SubType = table.Column<string>(type: "varchar(50)", nullable: true),
                    Account = table.Column<string>(type: "varchar(50)", nullable: true),
                    Name = table.Column<string>(type: "varchar(50)", nullable: true),
                    Describe = table.Column<string>(type: "varchar(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WechatSubLedgerReceivers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WechatOrders");

            migrationBuilder.DropTable(
                name: "WechatSubDetail");

            migrationBuilder.DropTable(
                name: "WechatSubLedgerReceivers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTime",
                table: "Members",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 12, 20, 9, 7, 253, DateTimeKind.Local).AddTicks(3200),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 1, 15, 22, 49, 7, 966, DateTimeKind.Local).AddTicks(1931));
        }
    }
}

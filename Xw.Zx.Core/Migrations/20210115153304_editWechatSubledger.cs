using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class editWechatSubledger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SubState",
                table: "WechatOrders",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTime",
                table: "Members",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 15, 23, 33, 3, 463, DateTimeKind.Local).AddTicks(5407),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 1, 15, 22, 49, 7, 966, DateTimeKind.Local).AddTicks(1931));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SubState",
                table: "WechatOrders",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTime",
                table: "Members",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 15, 22, 49, 7, 966, DateTimeKind.Local).AddTicks(1931),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 1, 15, 23, 33, 3, 463, DateTimeKind.Local).AddTicks(5407));
        }
    }
}

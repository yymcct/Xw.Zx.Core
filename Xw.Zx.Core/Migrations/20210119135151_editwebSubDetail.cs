using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class editwebSubDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SubState",
                table: "WechatOrders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTime",
                table: "Members",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 19, 21, 51, 50, 298, DateTimeKind.Local).AddTicks(3840),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 1, 18, 22, 0, 7, 24, DateTimeKind.Local).AddTicks(2290));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SubState",
                table: "WechatOrders",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTime",
                table: "Members",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 18, 22, 0, 7, 24, DateTimeKind.Local).AddTicks(2290),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 1, 19, 21, 51, 50, 298, DateTimeKind.Local).AddTicks(3840));
        }
    }
}

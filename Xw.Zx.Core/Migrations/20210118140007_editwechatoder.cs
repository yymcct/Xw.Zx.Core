using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class editwechatoder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTime",
                table: "Members",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 18, 22, 0, 7, 24, DateTimeKind.Local).AddTicks(2290),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 1, 15, 23, 49, 41, 509, DateTimeKind.Local).AddTicks(9115));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTime",
                table: "Members",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 15, 23, 49, 41, 509, DateTimeKind.Local).AddTicks(9115),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 1, 18, 22, 0, 7, 24, DateTimeKind.Local).AddTicks(2290));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class editMemberZxQrcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTime",
                table: "Members",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 24, 18, 30, 25, 539, DateTimeKind.Local).AddTicks(946),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 1, 19, 21, 51, 50, 298, DateTimeKind.Local).AddTicks(3840));

            migrationBuilder.AddColumn<string>(
                name: "ZxQRCode",
                table: "Members",
                type: "varchar(200)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZxQRCode",
                table: "Members");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTime",
                table: "Members",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 19, 21, 51, 50, 298, DateTimeKind.Local).AddTicks(3840),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 1, 24, 18, 30, 25, 539, DateTimeKind.Local).AddTicks(946));
        }
    }
}

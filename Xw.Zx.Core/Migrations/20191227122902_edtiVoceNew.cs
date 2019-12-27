using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class edtiVoceNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddTime",
                table: "VoiceNews",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddTime",
                table: "VoiceNews");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class addSysLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTime",
                table: "Members",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 15, 23, 49, 41, 509, DateTimeKind.Local).AddTicks(9115),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 1, 15, 23, 33, 3, 463, DateTimeKind.Local).AddTicks(5407));

            migrationBuilder.CreateTable(
                name: "SysLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    logType = table.Column<int>(nullable: false),
                    Log = table.Column<string>(nullable: true),
                    AdminName = table.Column<string>(nullable: true),
                    AdminId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SysLogs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTime",
                table: "Members",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 15, 23, 33, 3, 463, DateTimeKind.Local).AddTicks(5407),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 1, 15, 23, 49, 41, 509, DateTimeKind.Local).AddTicks(9115));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class addCiticbank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "CiticbankLogs");

            migrationBuilder.DropColumn(
                name: "SwiftPassUUID",
                table: "CiticbankLogs");

            migrationBuilder.AddColumn<string>(
                name: "MchId",
                table: "CiticbankLogs",
                type: "varchar(12)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Timestamp",
                table: "CiticbankLogs",
                type: "varchar(21)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UUID",
                table: "CiticbankLogs",
                type: "varchar(30)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CiticbankMchIds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    MemberId = table.Column<int>(nullable: false),
                    MchId = table.Column<string>(type: "varchar(12)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CiticbankMchIds", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CiticbankMchIds");

            migrationBuilder.DropColumn(
                name: "MchId",
                table: "CiticbankLogs");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "CiticbankLogs");

            migrationBuilder.DropColumn(
                name: "UUID",
                table: "CiticbankLogs");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "CiticbankLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SwiftPassUUID",
                table: "CiticbankLogs",
                nullable: true);
        }
    }
}

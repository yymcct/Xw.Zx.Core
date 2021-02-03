using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class RefactorCiticBanck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SwiftPassLogs");

            migrationBuilder.CreateTable(
                name: "CiticbankLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    SwiftPassUUID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CiticbankLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Xtsuo_Members",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Level = table.Column<int>(nullable: false),
                    AgentId = table.Column<int>(nullable: false),
                    openid = table.Column<string>(nullable: true),
                    RealName = table.Column<string>(nullable: true),
                    createtime = table.Column<int>(nullable: false),
                    NickName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Xtsuo_Members", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CiticbankLogs");

            migrationBuilder.DropTable(
                name: "Xtsuo_Members");

            migrationBuilder.CreateTable(
                name: "SwiftPassLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    OrderId = table.Column<int>(nullable: false),
                    SwiftPassUUID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SwiftPassLogs", x => x.Id);
                });
        }
    }
}

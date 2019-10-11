using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class addver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppPlatform = table.Column<int>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    DownLoadUrl = table.Column<string>(nullable: true),
                    WithPhones = table.Column<string>(nullable: true),
                    IsAllUpdate = table.Column<bool>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppVersions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppVersions");
        }
    }
}

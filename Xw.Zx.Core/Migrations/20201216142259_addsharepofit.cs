using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class addsharepofit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MemberVipType",
                table: "UpdateVipAuthCodes",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldDefaultValue: 10);

            migrationBuilder.CreateTable(
                name: "ShareProfitConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    ProductId = table.Column<int>(nullable: false),
                    ShareProfitTemplateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareProfitConfigs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShareProfitConfigs");

            migrationBuilder.AlterColumn<int>(
                name: "MemberVipType",
                table: "UpdateVipAuthCodes",
                nullable: false,
                defaultValue: 10,
                oldClrType: typeof(int),
                oldDefaultValue: 0);
        }
    }
}

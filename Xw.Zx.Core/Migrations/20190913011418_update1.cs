using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InviteId",
                table: "Members",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MemberVipType",
                table: "Members",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InviteId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "MemberVipType",
                table: "Members");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class addidentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityCardImgFront",
                table: "Members",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityCardImgReverse",
                table: "Members",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityCardNum",
                table: "Members",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemberIdentityState",
                table: "Members",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityCardImgFront",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "IdentityCardImgReverse",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "IdentityCardNum",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "MemberIdentityState",
                table: "Members");
        }
    }
}

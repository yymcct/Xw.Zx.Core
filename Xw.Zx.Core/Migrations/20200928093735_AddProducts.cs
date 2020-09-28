using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class AddProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Check",
                table: "Products",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalesVolume",
                table: "Products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Check",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SalesVolume",
                table: "Products");
        }
    }
}

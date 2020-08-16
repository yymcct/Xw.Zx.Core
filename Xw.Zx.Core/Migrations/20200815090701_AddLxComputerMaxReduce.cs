using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class AddLxComputerMaxReduce : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaxReduce",
                table: "LxComputers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MinReduce",
                table: "LxComputers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxReduce",
                table: "LxComputers");

            migrationBuilder.DropColumn(
                name: "MinReduce",
                table: "LxComputers");
        }
    }
}

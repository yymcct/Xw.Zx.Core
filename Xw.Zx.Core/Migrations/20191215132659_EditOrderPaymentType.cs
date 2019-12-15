using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class EditOrderPaymentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderPaymentType",
                table: "Orders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderPaymentType",
                table: "Orders");
        }
    }
}

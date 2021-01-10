using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class editCouponreceiveCoupon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CouponReceives_Couponid",
                table: "CouponReceives",
                column: "Couponid");

            migrationBuilder.AddForeignKey(
                name: "FK_CouponReceives_Coupons_Couponid",
                table: "CouponReceives",
                column: "Couponid",
                principalTable: "Coupons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CouponReceives_Coupons_Couponid",
                table: "CouponReceives");

            migrationBuilder.DropIndex(
                name: "IX_CouponReceives_Couponid",
                table: "CouponReceives");
        }
    }
}

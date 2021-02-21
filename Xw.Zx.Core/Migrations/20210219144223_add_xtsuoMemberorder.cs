using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xw.Zx.Core.Migrations
{
    public partial class add_xtsuoMemberorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Xtsuo_MemberOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OpenId = table.Column<string>(nullable: true),
                    AgentId = table.Column<int>(nullable: false),
                    ordersn = table.Column<string>(nullable: true),
                    price = table.Column<decimal>(nullable: false),
                    status = table.Column<string>(nullable: true),
                    paytime = table.Column<string>(nullable: true),
                    createtime = table.Column<string>(nullable: true),
                    paytype = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Xtsuo_MemberOrders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Xtsuo_MemberOrders");
        }
    }
}

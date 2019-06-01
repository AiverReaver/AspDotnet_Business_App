using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessApp.API.Migrations
{
    public partial class AddedPaytmEnitityClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaytmOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(nullable: false),
                    TXNID = table.Column<string>(nullable: true),
                    BANKTXNID = table.Column<string>(nullable: true),
                    ORDERID = table.Column<string>(nullable: true),
                    TXNAMOUNT = table.Column<string>(nullable: true),
                    STATUS = table.Column<string>(nullable: true),
                    TXNTYPE = table.Column<string>(nullable: true),
                    GATEWAYNAME = table.Column<string>(nullable: true),
                    RESPCODE = table.Column<string>(nullable: true),
                    RESPMSG = table.Column<string>(nullable: true),
                    BANKNAME = table.Column<string>(nullable: true),
                    MID = table.Column<string>(nullable: true),
                    PAYMENTMODE = table.Column<string>(nullable: true),
                    REFUNDAMT = table.Column<string>(nullable: true),
                    TXNDATE = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaytmOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaytmOrders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaytmOrders_UserId",
                table: "PaytmOrders",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaytmOrders");
        }
    }
}

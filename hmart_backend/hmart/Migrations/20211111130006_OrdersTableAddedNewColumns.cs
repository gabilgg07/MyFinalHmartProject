using Microsoft.EntityFrameworkCore.Migrations;

namespace hmart.Migrations
{
    public partial class OrdersTableAddedNewColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsExpress",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ShippingPrice",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExpress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingPrice",
                table: "Orders");
        }
    }
}

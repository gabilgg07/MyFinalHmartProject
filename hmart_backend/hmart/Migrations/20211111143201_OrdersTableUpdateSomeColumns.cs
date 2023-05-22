using Microsoft.EntityFrameworkCore.Migrations;

namespace hmart.Migrations
{
    public partial class OrdersTableUpdateSomeColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Orders",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StateOrRegion",
                table: "Orders",
                maxLength: 35,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipOrPostalCode",
                table: "Orders",
                maxLength: 25,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StateOrRegion",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ZipOrPostalCode",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Orders",
                type: "nvarchar(35)",
                maxLength: 35,
                nullable: true);
        }
    }
}

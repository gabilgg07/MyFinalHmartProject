using Microsoft.EntityFrameworkCore.Migrations;

namespace hmart.Migrations
{
    public partial class PartnersAndTeamsTablesAddedOrderColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Teams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Partners",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Partners");
        }
    }
}

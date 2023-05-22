using Microsoft.EntityFrameworkCore.Migrations;

namespace hmart.Migrations
{
    public partial class FeaturesTableAddedOrderColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "Partners",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Features",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Features");

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "Partners",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}

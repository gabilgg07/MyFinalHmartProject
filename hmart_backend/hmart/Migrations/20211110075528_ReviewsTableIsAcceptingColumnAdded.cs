using Microsoft.EntityFrameworkCore.Migrations;

namespace hmart.Migrations
{
    public partial class ReviewsTableIsAcceptingColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAccepting",
                table: "Reviews",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepting",
                table: "Reviews");
        }
    }
}

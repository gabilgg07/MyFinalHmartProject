using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hmart.Migrations
{
    public partial class BannerIsMainColumnChangedNulables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutBlogDesc",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "AboutBlogTitle",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "AboutTestiDesc",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "AboutTestiTitle",
                table: "Settings");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "BlogSubComments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "BlogComments",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsMain",
                table: "Banners",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rate = table.Column<double>(nullable: true),
                    Message = table.Column<string>(maxLength: 700, nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    AppUserId = table.Column<int>(nullable: false),
                    AppUserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_AspNetUsers_AppUserId1",
                        column: x => x.AppUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Review_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Review_AppUserId1",
                table: "Review",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Review_ProductId",
                table: "Review",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "BlogSubComments");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "BlogComments");

            migrationBuilder.AddColumn<string>(
                name: "AboutBlogDesc",
                table: "Settings",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutBlogTitle",
                table: "Settings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutTestiDesc",
                table: "Settings",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutTestiTitle",
                table: "Settings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsMain",
                table: "Banners",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}

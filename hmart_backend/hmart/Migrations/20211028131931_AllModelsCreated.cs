using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hmart.Migrations
{
    public partial class AllModelsCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutBlogDesc",
                table: "Settings",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutBlogTitle",
                table: "Settings",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutDesc",
                table: "Settings",
                maxLength: 700,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutSubtitle",
                table: "Settings",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutTeamDesc",
                table: "Settings",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutTeamTitle",
                table: "Settings",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutTestiDesc",
                table: "Settings",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutTestiTitle",
                table: "Settings",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutTitle",
                table: "Settings",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PromoImgSrc",
                table: "Settings",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PromoVideoLink",
                table: "Settings",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Banners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 100, nullable: true),
                    Price = table.Column<double>(nullable: true),
                    IsMain = table.Column<bool>(nullable: true),
                    Image = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 150, nullable: true),
                    Image = table.Column<string>(maxLength: 100, nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    Author = table.Column<string>(maxLength: 60, nullable: true),
                    Desc = table.Column<string>(maxLength: 600, nullable: true),
                    Text = table.Column<string>(maxLength: 6000, nullable: true),
                    Quote = table.Column<string>(maxLength: 250, nullable: true),
                    Quoter = table.Column<string>(maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 100, nullable: true),
                    Subtitle = table.Column<string>(maxLength: 150, nullable: true),
                    Image = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(maxLength: 100, nullable: true),
                    Position = table.Column<string>(maxLength: 100, nullable: true),
                    Image = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Testimonials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Author = table.Column<string>(maxLength: 50, nullable: true),
                    Position = table.Column<string>(maxLength: 50, nullable: true),
                    Text = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testimonials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(maxLength: 100, nullable: true),
                    Message = table.Column<string>(maxLength: 600, nullable: true),
                    BlogId = table.Column<int>(nullable: false),
                    AppUserId = table.Column<int>(nullable: false),
                    AppUserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogComments_AspNetUsers_AppUserId1",
                        column: x => x.AppUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogComments_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogTagBlogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogId = table.Column<int>(nullable: false),
                    BlogTagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTagBlogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogTagBlogs_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogTagBlogs_BlogTags_BlogTagId",
                        column: x => x.BlogTagId,
                        principalTable: "BlogTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Image = table.Column<string>(maxLength: 100, nullable: true),
                    Desc = table.Column<string>(maxLength: 800, nullable: true),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CostPrice = table.Column<double>(nullable: true),
                    Price = table.Column<double>(nullable: true),
                    DiscountPercent = table.Column<int>(nullable: true),
                    Rate = table.Column<double>(nullable: true),
                    Weight = table.Column<string>(maxLength: 50, nullable: true),
                    Dimensions = table.Column<string>(maxLength: 50, nullable: true),
                    Materials = table.Column<string>(maxLength: 100, nullable: true),
                    OtherInfo = table.Column<string>(maxLength: 200, nullable: true),
                    IsNew = table.Column<bool>(nullable: true),
                    IsAvailable = table.Column<bool>(nullable: true),
                    IsOnOffer = table.Column<bool>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    BrandId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogSubComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(maxLength: 100, nullable: true),
                    Message = table.Column<string>(maxLength: 600, nullable: true),
                    BlogCommentId = table.Column<int>(nullable: false),
                    AppUserId = table.Column<int>(nullable: false),
                    AppUserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogSubComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogSubComments_AspNetUsers_AppUserId1",
                        column: x => x.AppUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogSubComments_BlogComments_BlogCommentId",
                        column: x => x.BlogCommentId,
                        principalTable: "BlogComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    IsMain = table.Column<bool>(nullable: true),
                    Predecessor = table.Column<string>(maxLength: 100, nullable: true),
                    SupporType = table.Column<string>(maxLength: 100, nullable: true),
                    Cushioning = table.Column<string>(maxLength: 100, nullable: true),
                    OfferTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductColors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    ColorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductColors_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductColors_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTagProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    ProductTagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTagProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTagProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTagProducts_ProductTags_ProductTagId",
                        column: x => x.ProductTagId,
                        principalTable: "ProductTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_AppUserId1",
                table: "BlogComments",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_BlogId",
                table: "BlogComments",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogSubComments_AppUserId1",
                table: "BlogSubComments",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_BlogSubComments_BlogCommentId",
                table: "BlogSubComments",
                column: "BlogCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTagBlogs_BlogId",
                table: "BlogTagBlogs",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTagBlogs_BlogTagId",
                table: "BlogTagBlogs",
                column: "BlogTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_ProductId",
                table: "Offers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_ColorId",
                table: "ProductColors",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_ProductId",
                table: "ProductColors",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTagProducts_ProductId",
                table: "ProductTagProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTagProducts_ProductTagId",
                table: "ProductTagProducts",
                column: "ProductTagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banners");

            migrationBuilder.DropTable(
                name: "BlogSubComments");

            migrationBuilder.DropTable(
                name: "BlogTagBlogs");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "ProductColors");

            migrationBuilder.DropTable(
                name: "ProductTagProducts");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Testimonials");

            migrationBuilder.DropTable(
                name: "BlogComments");

            migrationBuilder.DropTable(
                name: "BlogTags");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductTags");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropColumn(
                name: "AboutBlogDesc",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "AboutBlogTitle",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "AboutDesc",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "AboutSubtitle",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "AboutTeamDesc",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "AboutTeamTitle",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "AboutTestiDesc",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "AboutTestiTitle",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "AboutTitle",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "PromoImgSrc",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "PromoVideoLink",
                table: "Settings");
        }
    }
}

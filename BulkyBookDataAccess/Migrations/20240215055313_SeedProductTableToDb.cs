using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BulkyBookSolution.BulkyBookDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductAuthor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductListPrice = table.Column<double>(type: "float", nullable: false),
                    ProductPriceOneToFifty = table.Column<double>(type: "float", nullable: false),
                    ProductPriceFiftyPlus = table.Column<double>(type: "float", nullable: false),
                    ProductPriceHundredPlus = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "ProductAuthor", "ProductDescription", "ProductISBN", "ProductListPrice", "ProductPriceFiftyPlus", "ProductPriceHundredPlus", "ProductPriceOneToFifty", "ProductTitle" },
                values: new object[,]
                {
                    { 1, "Billy Spark", "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincProductIDunt. ", "SWD9999001", 99.0, 85.0, 80.0, 90.0, "Fortune of Time" },
                    { 2, "Nancy Hoover", "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincProductIDunt. ", "CAW777777701", 40.0, 25.0, 20.0, 30.0, "Dark Skies" },
                    { 3, "Julian Button", "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincProductIDunt. ", "RITO5555501", 55.0, 40.0, 35.0, 50.0, "Vanish in the Sunset" },
                    { 4, "Abby Muscles", "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincProductIDunt. ", "WS3333333301", 70.0, 60.0, 55.0, 65.0, "Cotton Candy" },
                    { 5, "Ron Parker", "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincProductIDunt. ", "SOTJ1111111101", 30.0, 25.0, 20.0, 27.0, "Rock in the Ocean" },
                    { 6, "Laura Phantom", "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincProductIDunt. ", "FOT000000001", 25.0, 22.0, 20.0, 23.0, "Leaves and Wonders" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}

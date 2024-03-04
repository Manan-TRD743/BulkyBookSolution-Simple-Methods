using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulkyBookSolution.BulkyBookDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForgienKeyReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Products_ProductId",
                table: "ShoppingCarts");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ShoppingCarts",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "ProductCount",
                table: "ShoppingCarts",
                newName: "ProductCount");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCarts_ProductId",
                table: "ShoppingCarts",
                newName: "IX_ShoppingCarts_ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Products_ProductID",
                table: "ShoppingCarts",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Products_ProductID",
                table: "ShoppingCarts");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "ShoppingCarts",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "ProductCount",
                table: "ShoppingCarts",
                newName: "PorductCount");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCarts_ProductID",
                table: "ShoppingCarts",
                newName: "IX_ShoppingCarts_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Products_ProductId",
                table: "ShoppingCarts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulkyBookSolution.BulkyBookDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderDetailsToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderDetailId",
                table: "OrderDetails",
                newName: "OrderDetailID");

            migrationBuilder.RenameColumn(
                name: "OrderPrice",
                table: "OrderDetails",
                newName: "OrderPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderDetailID",
                table: "OrderDetails",
                newName: "OrderDetailId");

            migrationBuilder.RenameColumn(
                name: "OrderPrice",
                table: "OrderDetails",
                newName: "OrderPrice");
        }
    }
}

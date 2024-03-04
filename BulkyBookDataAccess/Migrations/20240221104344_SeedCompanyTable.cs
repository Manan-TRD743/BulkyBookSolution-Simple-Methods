using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BulkyBookSolution.BulkyBookDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedCompanyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyID", "City", "CompanyName", "PhoneNumber", "PostalCode", "State", "StreetAddress" },
                values: new object[,]
                {
                    { 1, "New York", "ABC Corporation", "555-123-4567", "10001", "NY", "123 Main St" },
                    { 2, "Los Angeles", "XYZ Inc.", "555-987-6543", "90001", "CA", "456 Elm St" },
                    { 3, "Chicago", "123 Industries", "555-555-5555", "60601", "IL", "789 Oak St" },
                    { 4, "Miami", "Smith & Co.", "555-321-9876", "33101", "FL", "101 Pine St" },
                    { 5, "San Francisco", "Acme Enterprises", "555-888-9999", "94101", "CA", "202 Maple St" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyID",
                keyValue: 5);

            
        }
    }
}

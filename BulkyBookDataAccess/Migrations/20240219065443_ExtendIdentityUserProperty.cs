using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulkyBookSolution.BulkyBookDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ExtendIdentityUserProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserCity",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserPostalCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserState",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserStreetAddress",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUserCity",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserPostalCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserState",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserStreetAddress",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}

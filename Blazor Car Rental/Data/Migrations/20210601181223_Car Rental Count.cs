using Microsoft.EntityFrameworkCore.Migrations;

namespace Blazor_Car_Rental.Data.Migrations
{
    public partial class CarRentalCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "rentalcount",
                table: "Cars",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rentalcount",
                table: "Cars");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Blazor_Car_Rental.Data.Migrations
{
    public partial class RentalPaidAttributeadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "Rentals",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Rentals");
        }
    }
}

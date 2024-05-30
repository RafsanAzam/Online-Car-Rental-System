using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Car_Rental_System.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationViewModelAddFieldsToReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasValidDriverLicense",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasValidDriverLicense",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Reservations");
        }
    }
}

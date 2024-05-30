using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Car_Rental_System.Migrations
{
    /// <inheritdoc />
    public partial class AddSessionIDtoReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Reservations");
        }
    }
}

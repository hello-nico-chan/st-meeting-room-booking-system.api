using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingRoomBookingSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "MeetingRooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "MeetingRooms");
        }
    }
}

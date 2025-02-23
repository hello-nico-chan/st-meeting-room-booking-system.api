using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingRoomBookingSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ExpandProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "MeetingRooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "MeetingRooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "MeetingRooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Participants",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "MeetingRooms");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "MeetingRooms");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "MeetingRooms");

            migrationBuilder.DropColumn(
                name: "Participants",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Bookings");
        }
    }
}

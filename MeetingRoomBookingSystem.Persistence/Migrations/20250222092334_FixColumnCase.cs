using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingRoomBookingSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixColumnCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "Users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "RefreshTokens",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "RefreshTokens",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "MeetingRooms",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "MeetingRooms",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Users",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Users",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "RefreshTokens",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "RefreshTokens",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "MeetingRooms",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "MeetingRooms",
                newName: "createdAt");
        }
    }
}

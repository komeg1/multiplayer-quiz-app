using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiplayerQuizGame.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoomCode",
                table: "Rooms",
                newName: "Room_Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Room_Code",
                table: "Rooms",
                newName: "RoomCode");
        }
    }
}

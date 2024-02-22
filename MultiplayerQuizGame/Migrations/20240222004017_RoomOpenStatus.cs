using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiplayerQuizGame.Migrations
{
    /// <inheritdoc />
    public partial class RoomOpenStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOpen",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOpen",
                table: "Rooms");
        }
    }
}

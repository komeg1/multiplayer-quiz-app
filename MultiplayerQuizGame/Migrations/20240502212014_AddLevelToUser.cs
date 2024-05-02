using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiplayerQuizGame.Migrations
{
    /// <inheritdoc />
    public partial class AddLevelToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Experience",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "User");
        }
    }
}

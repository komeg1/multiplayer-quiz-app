using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiplayerQuizGame.Migrations
{
    /// <inheritdoc />
    public partial class AddExperienceToStamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "UserQuizStamp",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Experience",
                table: "UserQuizStamp");
        }
    }
}

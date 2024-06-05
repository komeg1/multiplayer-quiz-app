using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiplayerQuizGame.Migrations
{
    /// <inheritdoc />
    public partial class AddAvatarFilenameFieldToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvatarB64",
                table: "User",
                newName: "AzureAvatarName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AzureAvatarName",
                table: "User",
                newName: "AvatarB64");
        }
    }
}

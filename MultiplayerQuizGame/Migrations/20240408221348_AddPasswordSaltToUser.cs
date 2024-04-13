using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiplayerQuizGame.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordSaltToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserQuizStamp_Quizzes_quizId",
                table: "UserQuizStamp");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Users",
                newName: "PasswordSalt");

            migrationBuilder.RenameColumn(
                name: "quizId",
                table: "UserQuizStamp",
                newName: "QuizId");

            migrationBuilder.RenameIndex(
                name: "IX_UserQuizStamp_quizId",
                table: "UserQuizStamp",
                newName: "IX_UserQuizStamp_QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuizStamp_Quizzes_QuizId",
                table: "UserQuizStamp",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserQuizStamp_Quizzes_QuizId",
                table: "UserQuizStamp");

            migrationBuilder.RenameColumn(
                name: "PasswordSalt",
                table: "Users",
                newName: "UserEmail");

            migrationBuilder.RenameColumn(
                name: "QuizId",
                table: "UserQuizStamp",
                newName: "quizId");

            migrationBuilder.RenameIndex(
                name: "IX_UserQuizStamp_QuizId",
                table: "UserQuizStamp",
                newName: "IX_UserQuizStamp_quizId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuizStamp_Quizzes_quizId",
                table: "UserQuizStamp",
                column: "quizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

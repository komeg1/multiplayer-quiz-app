using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiplayerQuizGame.Migrations
{
    /// <inheritdoc />
    public partial class AddUserQuizStamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Choices_Questions_QuestionId",
                table: "Question_Choices");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionQuiz_Questions_QuestionsId",
                table: "QuestionQuiz");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionQuiz_Quizzes_QuizzesId",
                table: "QuestionQuiz");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizRoom_Quizzes_QuizesId",
                table: "QuizRoom");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizRoom_Rooms_RoomsId",
                table: "QuizRoom");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomUser_Rooms_RoomsId",
                table: "RoomUser");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomUser_Users_PlayersId",
                table: "RoomUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quizzes",
                table: "Quizzes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Question_Choices",
                table: "Question_Choices");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Rooms",
                newName: "Room");

            migrationBuilder.RenameTable(
                name: "Quizzes",
                newName: "Quiz");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "Question");

            migrationBuilder.RenameTable(
                name: "Question_Choices",
                newName: "QuestionChoice");

            migrationBuilder.RenameIndex(
                name: "IX_Question_Choices_QuestionId",
                table: "QuestionChoice",
                newName: "IX_QuestionChoice_QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Room",
                table: "Room",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quiz",
                table: "Quiz",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Question",
                table: "Question",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionChoice",
                table: "QuestionChoice",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserQuizStamp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserQuizStamp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserQuizStamp_Quiz_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserQuizStamp_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserQuizStamp_QuizId",
                table: "UserQuizStamp",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuizStamp_UserId",
                table: "UserQuizStamp",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionChoice_Question_QuestionId",
                table: "QuestionChoice",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionQuiz_Question_QuestionsId",
                table: "QuestionQuiz",
                column: "QuestionsId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionQuiz_Quiz_QuizzesId",
                table: "QuestionQuiz",
                column: "QuizzesId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizRoom_Quiz_QuizesId",
                table: "QuizRoom",
                column: "QuizesId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizRoom_Room_RoomsId",
                table: "QuizRoom",
                column: "RoomsId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomUser_Room_RoomsId",
                table: "RoomUser",
                column: "RoomsId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomUser_User_PlayersId",
                table: "RoomUser",
                column: "PlayersId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionChoice_Question_QuestionId",
                table: "QuestionChoice");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionQuiz_Question_QuestionsId",
                table: "QuestionQuiz");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionQuiz_Quiz_QuizzesId",
                table: "QuestionQuiz");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizRoom_Quiz_QuizesId",
                table: "QuizRoom");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizRoom_Room_RoomsId",
                table: "QuizRoom");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomUser_Room_RoomsId",
                table: "RoomUser");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomUser_User_PlayersId",
                table: "RoomUser");

            migrationBuilder.DropTable(
                name: "UserQuizStamp");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Room",
                table: "Room");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quiz",
                table: "Quiz");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionChoice",
                table: "QuestionChoice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Question",
                table: "Question");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Room",
                newName: "Rooms");

            migrationBuilder.RenameTable(
                name: "Quiz",
                newName: "Quizzes");

            migrationBuilder.RenameTable(
                name: "QuestionChoice",
                newName: "Question_Choices");

            migrationBuilder.RenameTable(
                name: "Question",
                newName: "Questions");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionChoice_QuestionId",
                table: "Question_Choices",
                newName: "IX_Question_Choices_QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quizzes",
                table: "Quizzes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Question_Choices",
                table: "Question_Choices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Choices_Questions_QuestionId",
                table: "Question_Choices",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionQuiz_Questions_QuestionsId",
                table: "QuestionQuiz",
                column: "QuestionsId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionQuiz_Quizzes_QuizzesId",
                table: "QuestionQuiz",
                column: "QuizzesId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizRoom_Quizzes_QuizesId",
                table: "QuizRoom",
                column: "QuizesId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizRoom_Rooms_RoomsId",
                table: "QuizRoom",
                column: "RoomsId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomUser_Rooms_RoomsId",
                table: "RoomUser",
                column: "RoomsId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomUser_Users_PlayersId",
                table: "RoomUser",
                column: "PlayersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

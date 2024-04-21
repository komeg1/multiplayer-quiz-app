using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiplayerQuizGame.Migrations
{
    /// <inheritdoc />
    public partial class RefactorRoomTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizRoom");

            migrationBuilder.RenameColumn(
                name: "Room_Code",
                table: "Room",
                newName: "RoomCode");

            migrationBuilder.RenameColumn(
                name: "Created_at",
                table: "Room",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<int>(
                name: "QuizId",
                table: "Room",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Room_QuizId",
                table: "Room",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Quiz_QuizId",
                table: "Room",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Quiz_QuizId",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_QuizId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "Room");

            migrationBuilder.RenameColumn(
                name: "RoomCode",
                table: "Room",
                newName: "Room_Code");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Room",
                newName: "Created_at");

            migrationBuilder.CreateTable(
                name: "QuizRoom",
                columns: table => new
                {
                    QuizesId = table.Column<int>(type: "int", nullable: false),
                    RoomsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizRoom", x => new { x.QuizesId, x.RoomsId });
                    table.ForeignKey(
                        name: "FK_QuizRoom_Quiz_QuizesId",
                        column: x => x.QuizesId,
                        principalTable: "Quiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizRoom_Room_RoomsId",
                        column: x => x.RoomsId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizRoom_RoomsId",
                table: "QuizRoom",
                column: "RoomsId");
        }
    }
}

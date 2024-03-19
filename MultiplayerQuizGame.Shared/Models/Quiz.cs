using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required DateTime Create_Date { get; set; }
        public required List<Question> Questions { get; set; }
        public List<Room>? Rooms { get; set; }
    }

    public class QuizInfo
    {
        public Quiz Quiz { get; set; }
        public int QuestionCount { get; set; }
    }
}
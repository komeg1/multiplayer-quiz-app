using MultiplayerQuizGame.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Models.DTO
{
    public class UserQuizStampDto
    {
        public int Id { get; set; }
        public int UserId {  get; set; }
        public QuizDto Quiz { get; set; } 
        public DateTime DateTime { get; set; }
        public int Points { get; set; } 
        public int Score { get; set; }
        public int Experience { get; set; }
        public GameMode GameMode { get; set; }
    }
}

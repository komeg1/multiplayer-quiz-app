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
        public int QuizId { get; set; }
        public DateTime DateTime { get; set; }
        public int Points { get; set; } 
    }
}

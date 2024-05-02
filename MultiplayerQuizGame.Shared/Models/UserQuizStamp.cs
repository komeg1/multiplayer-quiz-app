using MultiplayerQuizGame.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Models
{
    public class UserQuizStamp
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Quiz Quiz { get; set; }
        public int Points { get; set; }
        //Score is an amount of the correct answers
        public int Score { get; set; }
        //Experience gained in this specific quiz
        public int Experience {  get; set; }
        public GameMode GameMode { get; set; }
        public DateTime DateTime { get; set; }
    }
}

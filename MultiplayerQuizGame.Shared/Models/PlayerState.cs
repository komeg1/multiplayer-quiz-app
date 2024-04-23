using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Models
{
    public class PlayerState
    {
        public bool IsReady { get; set; }
        //Calculated based on remaining question time etc.
        public int Points { get; set; }
        //How many correct answers
        public int Score { get; set; }
    }
}

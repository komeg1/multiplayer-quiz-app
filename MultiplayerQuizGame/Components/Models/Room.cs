using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Components.Models
{
    public class Room
    {
        public int Id { get; set; }
        public required string Room_code { get; set; }
        public List<Quiz>? Quizes { get; set; }
        public List<User>? Players { get; set; }
    }
}
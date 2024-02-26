using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Models
{
    public class Room
    {
        public int Id { get; set; }
        public required string Room_Code { get; set; }
        public List<Quiz>? Quizes { get; set; }
        public List<User>? Players { get; set; }
        public required DateTime Created_at { get; set; }
        public bool IsOpen { get; set; }
    }
}
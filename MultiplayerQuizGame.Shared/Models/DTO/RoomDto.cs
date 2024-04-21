using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Models.DTO
{
    public class RoomDto
    {
        public string RoomCode { get; set; }
        public QuizDto Quiz { get; set; }
        public List<UserDto> Users { get; set; }
        //List of users that are not logged, but take part in the game
        //List of guests + List of users = list of players in the lobby 
        public List<Guest> Guests { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

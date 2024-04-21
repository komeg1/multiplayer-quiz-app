using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Models
{
    
    public class Room
    {
        public int Id { get; set; }
        public  string RoomCode { get; set; }
        public Quiz Quiz { get; set; }

        //List of users that were logged and joined to the game
        //Used for user's game history
        public List<User> Users { get; set; }
        //List of users that are not logged, but take part in the game
        //List of guests + List of users = list of players in the lobby 
        public List<Guest> Guests { get; set; }
        public  DateTime CreatedAt { get; set; }
        public bool IsOpen { get; set; }

    }


}
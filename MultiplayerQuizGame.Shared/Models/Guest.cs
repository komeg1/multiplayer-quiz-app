using Microsoft.EntityFrameworkCore;
using MultiplayerQuizGame.Shared.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Models
{
    
    public class Guest : IPlayer
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string ConnectionId { get; set; }
        Room Room { get; set; }


        
    }
}

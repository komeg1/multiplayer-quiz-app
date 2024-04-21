using MultiplayerQuizGame.Shared.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Models.DTO
{
    public class UserDto : IPlayer
    {
        public int Id { get; set; }
        public string Username { get; set; }
    }
}

using MultiplayerQuizGame.Shared.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Models
{
    public class PlayersInLobby
    {
        //Need to split because SingalR doesn't invoke the methods while passing interfaces?? (Wanted to pass List<IPlayer>)
        public List<UserDto> Users { get; set; } = new();
        public List<Guest> Guests { get; set; } = new();

        //Dictionary<ConnectionId, PlayerState>
        public Dictionary<string,PlayerState> PlayersStates { get; set; } = new();
        
    }
}

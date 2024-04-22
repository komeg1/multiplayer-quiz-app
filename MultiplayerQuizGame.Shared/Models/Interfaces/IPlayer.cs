using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Models.Interfaces
{
    public interface IPlayer
    {
        int Id { get; set; }
        string Username { get; set; }
        string ConnectionId { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiplayerQuizGame.Components.Models;

namespace MultiplayerQuizGame.Components.Repositories
{
    public interface IRoomRepository
    {
        Task<bool> AddRoomAsync(Room room);
        Task<Room> GetRoomByCodeAsync(string roomCode);
    }
}
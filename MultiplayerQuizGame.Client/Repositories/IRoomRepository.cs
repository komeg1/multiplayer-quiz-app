using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiplayerQuizGame.Shared.Models;

namespace MultiplayerQuizGame.Shared.Repositories
{
    public interface IRoomRepository
    {
        Task<bool> AddRoomAsync(Room room);
        Task<Room> GetRoomByCodeAsync(string roomCode);
    }
}
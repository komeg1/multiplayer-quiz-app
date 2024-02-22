using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiplayerQuizGame.Components.Models;
namespace MultiplayerQuizGame.Components.Services
{
    public interface IRoomService
    {
        Task<Room> CreateRoom();
        Task<Room> GetRoom(string roomCode);
        Task<Room> CloseRoom(string roomCode);
        Task<bool> IsCodeUnique(string roomCode);

        Task<string> GenerateRoomCode();

    
    }
}
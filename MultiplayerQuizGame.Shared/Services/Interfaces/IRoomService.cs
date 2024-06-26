using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiplayerQuizGame.Shared.Models;
namespace MultiplayerQuizGame.Shared.Services.Interfaces
{
    public interface IRoomService
    {
        Task<Room> CreateRoom(int quizId);
        Task<Room> GetRoom(string roomCode);
        Task<Room> CloseRoom(string roomCode);
        Task<bool> IsCodeUnique(string roomCode);
        Task<string> GenerateRoomCode();
        


    }
}
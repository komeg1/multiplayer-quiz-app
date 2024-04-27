using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiplayerQuizGame.Shared.Models;
using MultiplayerQuizGame.Shared.Models.DTO;
using MultiplayerQuizGame.Shared.Models.Interfaces;

namespace MultiplayerQuizGame.Shared.Repositories.Interfaces
{
    public interface IRoomRepository
    {
        Task<bool> AddRoomAsync(Room room);
        Task<Room> GetRoomByCodeAsync(string roomCode, bool includePlayers = false);
        //Deletes the player from room, and returns the room from which they have been removed
        Task<Room> RemovePlayerFromRoomAsync(string connectionId);
        Task<bool> TryAddUserToRoomAsync(string roomCode, IPlayer player);
        Task<List<RoomDto>> GetOpenRoomsDtoAsync();
        Task<Room> SetRoomToOpen(string roomCode, string hostConnectionId);
        Task<PlayersInLobby> GetPlayersInLobbyAsync(string roomCode);
    }
}
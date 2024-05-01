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
        Task<bool> TryAddUserToRoomAsync(string roomCode, IPlayer player);

        //Method used to check if the logged user that is connecting is already in the room
        Task<bool> IsPlayerInLobby(string roomCode, int userId);
        Task<Room> GetRoomByCodeAsync(string roomCode, bool includePlayers = false);
        Task<Room> SetRoomToOpen(string roomCode, string hostConnectionId);
        Task<IPlayer> GetPlayerByConnectionIdAsync(string connectionId);

        //Deletes the player from room, and returns the room from which they have been removed
        Task<Room> RemovePlayerFromRoomAsync(string connectionId);
        Task<List<RoomDto>> GetOpenRoomsDtoAsync();
        Task<PlayersInLobby> GetPlayersInLobbyAsync(string roomCode);
    }
}
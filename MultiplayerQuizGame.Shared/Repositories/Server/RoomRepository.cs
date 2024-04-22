using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using MultiplayerQuizGame.Shared.Data;
using MultiplayerQuizGame.Shared.Models;
using MultiplayerQuizGame.Shared.Models.DTO;
using MultiplayerQuizGame.Shared.Models.Interfaces;
using MultiplayerQuizGame.Shared.Repositories.Interfaces;

namespace MultiplayerQuizGame.Shared.Repositories.Server
{
    public class RoomRepository : IRoomRepository
    {
        private const int MAX_PLAYERS_IN_LOBBY = 4;
        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;
        public RoomRepository(DataContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }
        public async Task<bool> AddRoomAsync(Room room)
        {
            _context.Room.Add(room);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Room> GetRoomByCodeAsync(string roomCode, bool includePlayers = false)
        {
            if (includePlayers == false)
                return await _context.Room.FirstOrDefaultAsync(r => r.RoomCode == roomCode).ConfigureAwait(false);
            else
                return await _context.Room.
                        Include(r => r.Users).
                        Include(r => r.Guests).
                        FirstOrDefaultAsync(r => r.RoomCode == roomCode).ConfigureAwait(false);
        }

        public async Task<List<RoomDto>> GetOpenRoomsDtoAsync()
        {
            var openRooms = await _context.Room.
                Where(r=>r.IsOpen == true).
                ToListAsync();

            List<RoomDto> dtos = openRooms.Adapt<List<RoomDto>>();

            return dtos;
        }

        public async Task<Room> SetRoomToOpen(string roomCode)
        {
            var room = await _context.Room.
                FirstOrDefaultAsync(r => r.RoomCode == roomCode);

            if (room != null)
            {
                room.IsOpen = true;
            }
            await _context.SaveChangesAsync();

            return room;
        }

        public async Task<bool> TryAddUserToRoomAsync(string roomCode, IPlayer player)
        {
            var room = await GetRoomByCodeAsync(roomCode: roomCode, includePlayers: true);
            if (room == null)
                return false;

            if(room?.Users.ToList().Count + room?.Guests.ToList().Count >= MAX_PLAYERS_IN_LOBBY)
            {
                return false;
            }

            if (player is Guest)
            {
                room!.Guests.Add((Guest)player);
            }
            if (player is UserDto)
            {
                var user = await _userRepository.GetUserAsync(player.Id);
                user.ConnectionId = player.ConnectionId;
                room!.Users.Add(user);
            }

            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<PlayersInLobby> GetPlayersInLobbyAsync(string roomCode)
        {
            var room = await GetRoomByCodeAsync(roomCode: roomCode, includePlayers: true);
            if (room != null)
            {
                PlayersInLobby playersInLobby = new PlayersInLobby();
                foreach(var player in room.Users.ToList())
                {
                    playersInLobby.Users.Add(player.Adapt<UserDto>());
                    playersInLobby.IsReadyToPlayDict.Add(player.ConnectionId, false);
                }
                foreach(var player in room.Guests.ToList())
                {
                    playersInLobby.Guests.Add(player);
                    playersInLobby.IsReadyToPlayDict.Add(player.ConnectionId, false);
                }

                return playersInLobby;
            }
            return null!;

            
        }
    }
}
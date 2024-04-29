using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
                return await _context.Room.
                    Include(r=>r.Quiz).
                    FirstOrDefaultAsync(r => r.RoomCode == roomCode).ConfigureAwait(false);
            else
                return await _context.Room.
                        Include(r=>r.Quiz).
                        Include(r => r.Users).
                        Include(r => r.Guests).
                        AsSplitQuery().
                        FirstOrDefaultAsync(r => r.RoomCode == roomCode).ConfigureAwait(false);
        }

        public async Task<Room> RemovePlayerFromRoomAsync(string connectionId)
        {
            
            var room = await _context.Room.
                Include(r=>r.Users).
                Include(r=>r.Guests).
                Where(r => r.Users!.
                Any(u => u.ConnectionId == connectionId)).
                AsSplitQuery().
                FirstOrDefaultAsync();

            if(room != null)
            {
                var playerToRemove = room.Users.FirstOrDefault(player => player.ConnectionId == connectionId);
                room.Users.Remove(playerToRemove);
                if (playerToRemove.ConnectionId == room.HostConnectionId)
                {
                    if(room.Users != null && room.Users.Count > 0)
                    {
                        room.HostConnectionId = room.Users[0].ConnectionId;
                    }
                    else if(room.Guests != null && room.Guests.Count > 0)
                    {
                        room.HostConnectionId = room.Guests[0].ConnectionId;
                    }
                    else
                    {
                        room.IsOpen = false;
                    }
                }
                await _context.SaveChangesAsync();
                return room;
            }

            room = await _context.Room.
                Include(r => r.Users).
                Include(r => r.Guests).
                Where(r => r.Guests!.
                Any(u => u.ConnectionId == connectionId)).
                FirstOrDefaultAsync();
            if (room != null)
            {
                var guestToRemove = room.Guests.FirstOrDefault(player => player.ConnectionId == connectionId);
                room.Guests.Remove(guestToRemove);
                if (guestToRemove.ConnectionId == room.HostConnectionId)
                {
                    if (room.Users != null && room.Users.Count > 0)
                    {
                        room.HostConnectionId = room.Users[0].ConnectionId;
                    }
                    else if (room.Guests != null && room.Guests.Count > 0)
                    {
                        room.HostConnectionId = room.Guests[0].ConnectionId;
                    }
                    else
                    {
                        room.IsOpen = false;
                    }
                }
                await _context.SaveChangesAsync();
                return room;
            }
            return null;

        }

        public async Task<List<RoomDto>> GetOpenRoomsDtoAsync()
        {
            var openRooms = await _context.Room.
                Where(r=>r.IsOpen == true).
                ToListAsync();

            List<RoomDto> dtos = openRooms.Adapt<List<RoomDto>>();

            return dtos;
        }

        public async Task<Room> SetRoomToOpen(string roomCode, string hostConnectionId)
        {
            var room = await _context.Room.
                FirstOrDefaultAsync(r => r.RoomCode == roomCode);

            if (room != null)
            {
                room.IsOpen = true;
                room.HostConnectionId = hostConnectionId;
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

        public async Task<bool> TryChangeUserConnectionId(string roomCode, string connectionId, int userId)
        {
            var userToChange = await _userRepository.GetUserAsync(userId);
            if(userToChange != null)
            {
                var room = await GetRoomByCodeAsync(roomCode);

                if(room == null) return false;

                if(room.HostConnectionId == userToChange.ConnectionId)
                {
                    room.HostConnectionId = connectionId;
                }

                userToChange.ConnectionId = connectionId;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
            

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
                    playersInLobby.PlayersStates[player.ConnectionId!] =new PlayerState
                    {
                        IsReady = false,
                        Points = 0,
                        Score = 0
                    };
                }
                foreach(var player in room.Guests.ToList())
                {
                    playersInLobby.Guests.Add(player);
                    playersInLobby.PlayersStates[player.ConnectionId!] = new PlayerState
                    {
                        IsReady = false,
                        Points = 0,
                        Score = 0
                    };
                }

                return playersInLobby;
            }
            return null!;

            
        }
    }
}
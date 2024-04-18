using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MultiplayerQuizGame.Shared.Data;
using MultiplayerQuizGame.Shared.Models;
using MultiplayerQuizGame.Shared.Repositories.Interfaces;

namespace MultiplayerQuizGame.Shared.Repositories.Server
{
    public class RoomRepository : IRoomRepository
    {
        private readonly DataContext _context;
        public RoomRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> AddRoomAsync(Room room)
        {
            _context.Room.Add(room);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Room> GetRoomByCodeAsync(string roomCode)
        {
            Console.WriteLine($"Room code: {roomCode}");
            return await _context.Room.FirstOrDefaultAsync(r => r.Room_Code == roomCode).ConfigureAwait(false);
        }
    }
}
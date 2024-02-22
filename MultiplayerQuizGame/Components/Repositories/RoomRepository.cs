using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MultiplayerQuizGame.Components.Data;
using MultiplayerQuizGame.Components.Models;

namespace MultiplayerQuizGame.Components.Repositories
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
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Room> GetRoomByCodeAsync(string roomCode)
        {
            Console.WriteLine($"Room code: {roomCode}");
            return await _context.Rooms.FirstOrDefaultAsync(r => r.Room_code == roomCode).ConfigureAwait(false);
        }
    }
}
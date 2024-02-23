using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MultiplayerQuizGame.Components.Hubs;
using MultiplayerQuizGame.Components.Models;
using MultiplayerQuizGame.Components.Repositories;

namespace MultiplayerQuizGame.Components.Services
{
    public class RoomService : IRoomService
    {
        private Random _random;
        private readonly IRoomRepository _roomRepository;
        private HubConnection? _hubConnection;
        private const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly NavigationManager _navigationManager;
        private const int ROOM_CODE_LENGTH = 5;
        public RoomService(IRoomRepository roomRepository, NavigationManager navigationManager){
            _random = new Random();
            _roomRepository = roomRepository;
            _navigationManager = navigationManager;
        }
        public Task<Room> CloseRoom(string roomCode)
        {
            throw new NotImplementedException();
        }

        public async Task<Room> CreateRoom(UserConnection userConnection)
        {
            string roomCode = await GenerateRoomCode();
            Room room = new Room { Room_code = roomCode, Created_at = DateTime.Now, IsOpen = true };
            userConnection.RoomCode = roomCode;
            if(await _roomRepository.AddRoomAsync(room))
               {
                _hubConnection = new HubConnectionBuilder()
                                .WithUrl(_navigationManager.ToAbsoluteUri("/room"))
                                .Build();
                await _hubConnection.StartAsync();
                await _hubConnection.InvokeAsync("JoinRoom",userConnection);
                return room;
               }
            return null;
        }

        public async Task<string> GenerateRoomCode()
        {
            StringBuilder builder = new StringBuilder();
            do
            {
                builder.Clear();
                for (int i = 0; i < ROOM_CODE_LENGTH; i++)
                {
                    builder.Append(CHARS[_random.Next(CHARS.Length)]);
                }
            }
            while(await IsCodeUnique(builder.ToString()) == null);
            return builder.ToString();
        }

        public Task<Room> GetRoom(string roomCode)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsCodeUnique(string roomCode)
        {
            if (roomCode == "0")
                return false;
                
            var room = _roomRepository.GetRoomByCodeAsync(roomCode).Result;
            if (room != default(Room))
            {
                if(room.IsOpen)
                    return false;
                return true;
            }
            return true; 
        }
    }
}

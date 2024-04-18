using MultiplayerQuizGame.Shared.Models;
using MultiplayerQuizGame.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Services
{
    public class ClientRoomService : IRoomService
    {
        private readonly HttpClient _httpClient;
        public ClientRoomService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task<Room> CloseRoom(string roomCode)
        {
            throw new NotImplementedException();
        }

        public Task<Room> CreateRoom()
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateRoomCode()
        {
            throw new NotImplementedException();
        }

        public async Task<Room> GetRoom(string roomCode)
        {
            var result = await _httpClient.GetFromJsonAsync<Room>($"/api/room");
            return result;
        }

        public Task<bool> IsCodeUnique(string roomCode)
        {
            throw new NotImplementedException();
        }
    }
}

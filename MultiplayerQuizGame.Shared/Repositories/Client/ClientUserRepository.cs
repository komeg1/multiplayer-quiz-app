using MultiplayerQuizGame.Shared.Models;
using MultiplayerQuizGame.Shared.Models.DTO;
using MultiplayerQuizGame.Shared.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Repositories.Client
{
    public class ClientUserRepository : IUserRepository
    {
        private readonly HttpClient _httpClient;
        public ClientUserRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<UserQuizStampDto> SaveQuizStamp(UserQuizStampDto stampDto)
        {
            var result = await _httpClient.PostAsJsonAsync<UserQuizStampDto>($"/api/user/{stampDto.UserId}/stamp", stampDto);

            return await result.Content.ReadFromJsonAsync<UserQuizStampDto>();
        }

    }
}

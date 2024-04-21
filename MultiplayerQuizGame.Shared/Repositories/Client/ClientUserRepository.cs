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

        public async Task<UserDto> GetLoggedUser()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<UserDto>("/api/user");
                return result!;
            }
            catch (Exception ex)
            {
                return null!;
            }
            
        }
        public Task<User> GetUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserQuizStampDto>> GetUserGameHistory(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserQuizStampDto> SaveQuizStamp(int quizId, int userId = 0)
        {
            UserQuizStampDto stampDto = new UserQuizStampDto
            {
                UserId = userId,
                QuizId = quizId,
            };
            var result = await _httpClient.PostAsJsonAsync<UserQuizStampDto>($"/api/user/stamp", stampDto);

            return await result.Content.ReadFromJsonAsync<UserQuizStampDto>();
        }

        public async Task UpdateStampPoints(int stampId, int points)
        {
            var result = await _httpClient.PostAsJsonAsync<int>($"/api/stamp/{stampId}", points);
            var res = result.Content.ReadAsStringAsync();
            return;
        }

    }
}

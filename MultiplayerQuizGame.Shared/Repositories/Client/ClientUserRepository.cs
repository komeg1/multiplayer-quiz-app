﻿using MultiplayerQuizGame.Shared.Models;
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
        private readonly IQuizRepository _quizRepostiory;
        public ClientUserRepository(HttpClient httpClient, IQuizRepository quizRepository)
        {
            _httpClient = httpClient;
            _quizRepostiory = quizRepository;
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
        public Task<User> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> GetUserDtoByIdAsync(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<UserDto>($"/api/user/{id}");
            return result!;
        }

        public Task<List<UserQuizStampDto>> GetUserGameHistory(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserQuizStampDto> SaveQuizStamp(UserQuizStampDto stampDto)
        {
            var result = await _httpClient.PostAsJsonAsync<UserQuizStampDto>($"/api/user/stamp", stampDto);

            return await result.Content.ReadFromJsonAsync<UserQuizStampDto>();
        }

        public async Task UpdateStampPoints(int stampId, int points, int score)
        {
            var parameters = new { Points = points, Score = score };
            var result = await _httpClient.PostAsJsonAsync<object>($"/api/stamp/{stampId}", parameters);
            var res = result.Content.ReadAsStringAsync();
            return;
        }

    }
}

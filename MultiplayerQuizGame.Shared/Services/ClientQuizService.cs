using MultiplayerQuizGame.Shared.Data;
using MultiplayerQuizGame.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Services
{
    public class ClientQuizService : IQuizService
    {
        private readonly HttpClient _httpClient;
        public ClientQuizService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<List<QuizInfo>> GetAvailableQuizzes()
        {
            var result = await _httpClient.GetFromJsonAsync<List<QuizInfo>>("/api/quiz/all");
            return result;
        }
        public async Task<Quiz> GetQuiz(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<Quiz>($"/api/quiz/{id}");
            return result;
        }

    }
}

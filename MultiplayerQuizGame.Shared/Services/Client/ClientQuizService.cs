using MultiplayerQuizGame.Shared.Models.DTO;
using MultiplayerQuizGame.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Services.Client
{
    public class ClientQuizService : IQuizService
    {
        private readonly HttpClient _httpClient;
        public ClientQuizService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> CheckAnswer(int questionId, QuestionChoiceDto pickedAnswer)
        {
            var result = await _httpClient.PostAsJsonAsync<QuestionChoiceDto>($"/api/quiz/question/{questionId}",pickedAnswer);
            return await result.Content.ReadFromJsonAsync<bool>();
        }
    }
}

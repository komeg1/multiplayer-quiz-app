using MultiplayerQuizGame.Shared.Data;
using MultiplayerQuizGame.Shared.Models;
using MultiplayerQuizGame.Shared.Models.DTO;
using MultiplayerQuizGame.Shared.Repositories.Interfaces;
using System.Net.Http.Json;


namespace MultiplayerQuizGame.Shared.Repositories.Client
{
    public class ClientQuizRepository : IQuizRepository
    {
        private readonly HttpClient _httpClient;
        public ClientQuizRepository(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<List<QuizDto>> GetAvailableQuizzesDtoAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<QuizDto>>("/api/quiz/all");
            return result;
        }
        public async Task<QuizDto> GetQuizDtoAsync(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<QuizDto>($"/api/quiz/{id}");
            return result;
        }

        public async Task<QuestionDto> GetQuestionDtoAsync(int quizId, int questionNr)
        {
            var result = await _httpClient.GetFromJsonAsync<QuestionDto>($"/api/quiz/{quizId}/question/{questionNr}");
            return result;
        }

        public Task<Question> GetQuestionAsync(int questionId)
        {
            throw new NotImplementedException();
        }

        public async Task<QuestionDto> GetQuestionDtoAsync(int questionId)
        {
            var result = await _httpClient.GetFromJsonAsync<QuestionDto>($"/api/quiz/question/{questionId}");
            return result;
        }

        public Task<Quiz> GetQuizAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

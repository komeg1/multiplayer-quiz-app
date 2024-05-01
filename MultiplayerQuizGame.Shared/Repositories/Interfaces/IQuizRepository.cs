using MultiplayerQuizGame.Shared.Models;
using MultiplayerQuizGame.Shared.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Repositories.Interfaces
{
    public interface IQuizRepository
    {
        Task<Question> GetQuestionAsync(int questionId);
        Task<Quiz> GetQuizAsync(int id);

        //DTO
        Task<List<QuizDto>> GetAvailableQuizzesDtoAsync();
        Task<QuestionDto> GetQuestionDtoAsync(int quizId, int questionNr);
        Task<QuestionDto> GetQuestionDtoAsync(int questionId);
        Task<QuizDto> GetQuizDtoAsync(int id);
    }
}

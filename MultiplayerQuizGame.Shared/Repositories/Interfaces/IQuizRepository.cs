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
        Task<Question> GetQuestion(int questionId);
        Task<Quiz> GetQuiz(int id);

        //DTO
        Task<List<QuizDto>> GetAvailableQuizzesDto();
        Task<QuestionDto> GetQuestionDto(int quizId, int questionNr);
        Task<QuestionDto> GetQuestionDto(int questionId);
        Task<QuizDto> GetQuizDto(int id);
    }
}

using MultiplayerQuizGame.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Services
{
    public interface IQuizService
    {
        Task<List<QuizInfo>> GetAvailableQuizzes();
        Task<Quiz> GetQuiz(int id);
    }
}

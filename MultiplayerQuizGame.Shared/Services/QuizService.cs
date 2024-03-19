using Microsoft.EntityFrameworkCore;
using MultiplayerQuizGame.Shared.Data;
using MultiplayerQuizGame.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Services
{
    public class QuizService : IQuizService
    {
        private readonly DataContext _dataContext;

        public QuizService(DataContext dataContext) 
        { 
            _dataContext = dataContext;
        }
        public async Task<List<QuizInfo>> GetAvailableQuizzes()
        {
            var quizzesWithQuestionCount = await _dataContext.Quizzes
            .Select(q => new QuizInfo
            {
                Quiz = q,
                QuestionCount = q.Questions.Count()
            })
            .ToListAsync();

            return quizzesWithQuestionCount;
        }

        public async Task<Quiz> GetQuiz(int id)
        {
            
            var quiz = await _dataContext.Quizzes.
                Where(q => q.Id == id).
                Include(q=>q.Questions).
                ThenInclude(q=>q.Question_Choices).
                FirstOrDefaultAsync();

            return quiz;
            
        }
    }
}

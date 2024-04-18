using Microsoft.EntityFrameworkCore;
using MultiplayerQuizGame.Shared.Data;
using MultiplayerQuizGame.Shared.Models;
using MultiplayerQuizGame.Shared.Models.DTO;
using MultiplayerQuizGame.Shared.Repositories.Interfaces;
using Mapster;
namespace MultiplayerQuizGame.Shared.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly DataContext _dataContext;

        public QuizRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Question> GetQuestion(int quizId, int questionNr)
        {
            var questions = await _dataContext.Quiz.
                Include(q => q.Questions).
                ThenInclude(q => q.QuestionChoices).
                Where(q => q.Id == quizId).
                Select(q => q.Questions).
               FirstOrDefaultAsync();
            if(questions is null)
            {
                return null!;
            }

            var question = questions[questionNr];

            return question;
        }
        public async Task<Question> GetQuestion(int questionId)
        {
            var question = await _dataContext.Question.
                Include(q => q.QuestionChoices).
                Where(q => q.Id == questionId).
                FirstOrDefaultAsync();

            return question;
        }
        public async Task<Quiz> GetQuiz(int id)
        {

            var quiz = await _dataContext.Quiz.
                Where(q => q.Id == id).
                Include(q => q.Questions).
                FirstOrDefaultAsync();


            return quiz;

        }


        // DTO methods
        public async Task<List<QuizDto>> GetAvailableQuizzesDto()
        {
            var quizzesWithQuestionCount = await _dataContext.Quiz.
                Include(q=>q.Questions).
                Select(q => new QuizDto
                {
                    Id = q.Id,
                    Title = q.Title,
                    Description = q.Description,
                    CreateDate = q.CreateDate,
                    QuestionCount = q.Questions.Count()
                })
            .ToListAsync();


            return quizzesWithQuestionCount;
        }
        public async Task<QuizDto> GetQuizDto(int id)
        {

            var quiz = await _dataContext.Quiz.
                Where(q => q.Id == id).
                Include(q => q.Questions).
                Select(q => new QuizDto
                {
                    Id = q.Id,
                    Title = q.Title,
                    Description = q.Description,
                    CreateDate = q.CreateDate,
                    QuestionCount = q.Questions.Count()
                }).
                FirstOrDefaultAsync();

            QuizDto quizDto = quiz.Adapt<QuizDto>();

            return quizDto;

        }
        public async Task<QuestionDto> GetQuestionDto(int questionId)
        {
            var question = await GetQuestion(questionId);
            QuestionDto questionDto = question.Adapt<QuestionDto>();

            return questionDto;
        }
        public async Task<QuestionDto> GetQuestionDto(int quizId, int questionNr)
        {
            var question = await GetQuestion(quizId, questionNr);
            QuestionDto questionDto = question.Adapt<QuestionDto>();

            return questionDto;
        }
    }
}

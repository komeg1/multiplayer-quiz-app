using MultiplayerQuizGame.Shared.Models.DTO;
using MultiplayerQuizGame.Shared.Repositories.Interfaces;
using MultiplayerQuizGame.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Services.Server
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<bool> CheckAnswer(int questionId, QuestionChoiceDto pickedAnswer)
        {
            var question = await _quizRepository.GetQuestionAsync(questionId);

            if (question == null)
                throw new KeyNotFoundException();

            var correctAnswer = question.QuestionChoices.Find(q => q.IsTrue.Equals(true));
            if (correctAnswer == null)
                throw new KeyNotFoundException();

            if (correctAnswer.Id == pickedAnswer.Id)
                return true;

            return false;


        }
    }
}

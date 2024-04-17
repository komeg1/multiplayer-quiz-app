using MultiplayerQuizGame.Shared.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Services.Interfaces
{
    public interface IQuizService
    {
        Task<bool> CheckAnswer(int questionId, QuestionChoiceDto pickedAnswer);
    }
}

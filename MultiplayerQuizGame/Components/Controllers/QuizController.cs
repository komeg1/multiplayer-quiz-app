using Microsoft.AspNetCore.Mvc;
using MultiplayerQuizGame.Shared.Models;
using MultiplayerQuizGame.Shared.Models.DTO;
using MultiplayerQuizGame.Shared.Repositories.Interfaces;
using MultiplayerQuizGame.Shared.Services.Interfaces;
namespace MultiplayerQuizGame.Components.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : Controller
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IQuizService _quizService;
        public QuizController(IQuizRepository quizRepository, IQuizService quizService)
        {
            _quizRepository = quizRepository;
            _quizService = quizService;
        }
        [HttpGet("all")]
        public async Task<ActionResult> GetAvailableQuizzes()
        {
            var quiz = await _quizRepository.GetAvailableQuizzesDto();
            return Ok(quiz);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetQuiz(string id)
        {
            var quiz = await _quizRepository.GetQuizDto(Int32.Parse(id));
            return Ok(quiz);
        }
        [HttpGet("{quizId}/question/{questionNr}")]
        public async Task<ActionResult> GetQuestion(int quizId, int questionNr)
        {
            var question = await _quizRepository.GetQuestionDto(quizId, questionNr);
            return Ok(question);
        }

        [HttpGet("/question/{questionId}")]
        public async Task<ActionResult> GetQuestion(int questionId)
        {
            var question = await _quizRepository.GetQuestionDto(questionId);
            return Ok(question);
        }

        [HttpPost("question/{questionId}")]
        public async Task<ActionResult> CheckAnswer(int questionId, [FromBody] QuestionChoiceDto pickedAnswer)
        {
            var result = await _quizService.CheckAnswer(questionId, pickedAnswer);
            return Ok(result);
        }






    }
}

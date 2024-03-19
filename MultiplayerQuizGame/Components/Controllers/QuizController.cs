using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiplayerQuizGame.Shared.Models;
using MultiplayerQuizGame.Shared.Services;

namespace MultiplayerQuizGame.Components.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;
        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }
        [HttpGet("/all")]
        public async Task<ActionResult<List<QuizInfo>>> GetAvailableQuizzes()
        {
            var quiz = await _quizService.GetAvailableQuizzes();
            return Ok(quiz);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz>> GetQuiz(string id)
        {
            var quiz = await _quizService.GetQuiz(Int32.Parse(id));
            return Ok(quiz);
        }
    }
}

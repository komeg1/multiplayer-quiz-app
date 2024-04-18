using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MultiplayerQuizGame.Shared.Services;
using MultiplayerQuizGame.Shared.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using MultiplayerQuizGame.Shared.Services.Interfaces;
using MultiplayerQuizGame.Shared.Repositories.Interfaces;

namespace MultiplayerQuizGame.Components.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet]
        [Authorize("LoggedUserOnly")]
        [Route("/logout")]
        public async Task Logout()
        {
            var prop = new AuthenticationProperties()
            {
                RedirectUri = "/",
            };
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme, prop);
        }

        [HttpPost]
        [Authorize("LoggedUserOnly")]
        [Route("{userId}/stamp")]
        public async Task<IActionResult> SaveQuizStamp(int userId, [FromBody] UserQuizStampDto stampDto)
        {
            try
            {
                var response = await _userRepository.SaveQuizStamp(stampDto);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }




    }

}

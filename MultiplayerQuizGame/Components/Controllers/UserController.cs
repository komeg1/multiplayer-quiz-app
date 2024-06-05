using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MultiplayerQuizGame.Shared.Services;
using MultiplayerQuizGame.Shared.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using MultiplayerQuizGame.Shared.Services.Interfaces;
using MultiplayerQuizGame.Shared.Repositories.Interfaces;
using MultiplayerQuizGame.Shared.Models;
using System.Security.Claims;
using MultiplayerQuizGame.Shared.Repositories;
using Mapster;

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
        [Route("stamp")]
        public async Task<IActionResult> SaveQuizStamp(UserQuizStampDto stampDto)
        {
            try
            {
                if (HttpContext.User.Identity is not null && HttpContext.User.Identity.IsAuthenticated)
                {
                    var userId = int.Parse(HttpContext.User.FindFirst(c => c.Type is "id")?.Value);
                    var username = HttpContext.User.FindFirst(c => c.Type is ClaimTypes.Name)?.Value;
                    Console.WriteLine($"{userId} oraz {username}");
                    stampDto.UserId = userId;
                    stampDto = await _userRepository.SaveQuizStamp(stampDto);
                    return Ok(stampDto);
                }
                return NotFound();
                
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        [HttpPost]
        [Authorize("LoggedUserOnly")]
        [Route("/api/stamp/{stampId}")]
        public async Task<IActionResult> UpdateStamp(int stampId,[FromBody] StampData data)
        {
            try
            { 
                await _userRepository.UpdateStamp(stampId, data.Points, data.Score, data.Experience);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        [HttpGet]
        [Authorize("LoggedUserOnly")]
        [Route("")]
        public async Task<IActionResult> GetLoggedUser()
        {
            if (HttpContext.User.Identity is not null && HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(HttpContext.User.FindFirst(c => c.Type is "id")?.Value);
                var user = await _userRepository.GetUserByIdAsync(userId);
                return Ok(user.Adapt<UserDto>());
            }
            return Ok(null);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserDto>> GetUserDtoByIdAsync(string id)
        {
            var result = await _userRepository.GetUserDtoByIdAsync(Int32.Parse(id));
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        public class StampData
        {
            public int Points { get; set; }
            public int Score { get; set; }
            public int Experience { get; set; }
        }
    }

}

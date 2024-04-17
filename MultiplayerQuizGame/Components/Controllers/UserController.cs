//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Mvc;
//using MultiplayerQuizGame.Shared.Services;
//using MultiplayerQuizGame.Shared.Models.DTO;
//using Microsoft.AspNetCore.Authorization;

//namespace MultiplayerQuizGame.Components.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : Controller
//    {
//        private readonly IUserService _userService;

//        public UserController(IUserService userService)
//        {
//            _userService = userService;
//        }


//        [HttpPost("register")]
//        public async Task<IActionResult> Register([FromBody] RegisterDto resource)
//        {
//            try
//            {
//                var response = await _userService.Register(resource);
//                return Ok(response);
//            }
//            catch (Exception e)
//            {
//                return BadRequest(new { ErrorMessage = e.Message });
//            }
//        }

//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] LoginDto resource)
//        {
//            try
//            {
//                var response = await _userService.Login(resource);
//                return Ok(response);
//            }
//            catch (Exception e)
//            {
//                return BadRequest(new { ErrorMessage = e.Message });
//            }
//        }

//        [Authorize("LoggedUserOnly")]
//        [Route("/logout")]
//        [NonAction]
//        public async Task Logout()
//        {
//            var prop = new AuthenticationProperties()
//            {
//                RedirectUri = "/",
//            };
//            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme, prop);


//        }

//        [Authorize("LoggedUserOnly")]
//        [HttpPost("{id}/stamp")]
//        public async Task<IActionResult> SaveUserStamp([FromBody] UserQuizStampDto dto)
//        {
            
//            try
//            {
//                await _userService.SaveQuizStamp(dto);
//                return Ok();
//            }
//            catch (Exception e)
//            {
//                return BadRequest(new { ErrorMessage = e.Message });
//            }
//        }




//    }

//}

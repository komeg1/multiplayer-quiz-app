﻿using Microsoft.AspNetCore.Authentication.Cookies;
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
                    stampDto = await _userRepository.SaveQuizStamp(stampDto.QuizId, userId);
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
        public async Task<IActionResult> UpdateStampPoints(int stampId,[FromBody] int points)
        {
            
            try
            { 
                await _userRepository.UpdateStampPoints(stampId, points);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }




    }

}

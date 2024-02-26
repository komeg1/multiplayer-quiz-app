using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiplayerQuizGame.Shared.Models;
using MultiplayerQuizGame.Shared.Services;

namespace MultiplayerQuizGame.Components.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        [HttpPost]
        public async void CreateRoom(string roomCode)
        {
            await _roomService.CreateRoom(roomCode);
        }

        [HttpGet]
        public async Task<ActionResult<Room>> GetRoom(string roomCode)
        {
            var room = await _roomService.GetRoom(roomCode);
            return Ok(room);
        }

    }
}

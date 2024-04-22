using Mapster;
using Microsoft.AspNetCore.SignalR;
using MultiplayerQuizGame.Shared.Models;
using MultiplayerQuizGame.Shared.Models.DTO;
using MultiplayerQuizGame.Shared.Models.Interfaces;
using MultiplayerQuizGame.Shared.Repositories.Interfaces;
using MultiplayerQuizGame.Shared.Repositories.Server;
using MultiplayerQuizGame.Shared.Services.Interfaces;
using MultiplayerQuizGame.Shared.Services.Server;

namespace MultiplayerQuizGame.Components.Hubs
{
    public class GameHub : Hub
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomService _roomService;
        private readonly IUserRepository _userRepository;
        public GameHub(IRoomRepository roomRepository, IRoomService roomService) 
        {
            _roomRepository = roomRepository;
            _roomService = roomService;

        }
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Player: {Context.ConnectionId}");

            var openRooms = await _roomRepository.GetOpenRoomsDtoAsync();

            await Clients.Caller.SendAsync("OpenRooms", openRooms.OrderBy(r=>r.CreatedAt));
        }

        public async Task<RoomDto?> OpenRoom(string roomCode, UserDto user = null!, Guest guest = null!)
        {
            var room = await _roomRepository.GetRoomByCodeAsync(roomCode);
            if (room == null)
                return null;

            if (room.IsOpen == true)
                return null;

            IPlayer player = user != null ? user : guest;

            await _roomRepository.TryAddUserToRoomAsync(roomCode, player);
            room = await _roomRepository.SetRoomToOpen(room.RoomCode);
            

            await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);

            var playersInLobby = await _roomRepository.GetPlayersInLobbyAsync(roomCode);
            
            await Clients.Group(roomCode).SendAsync("PlayerJoined", playersInLobby);

            var openRooms = await _roomRepository.GetOpenRoomsDtoAsync();
            await Clients.All.SendAsync("OpenRooms", openRooms.OrderBy(r => r.CreatedAt));

            room.HostConnectionId = Context.ConnectionId;
            return room.Adapt<RoomDto>();
        }

        public async Task<RoomDto?> JoinRoom(string roomCode, UserDto user = null!, Guest guest = null!)
        {
            var room = await _roomRepository.GetRoomByCodeAsync(roomCode);
            if (room == null)
                return null;

            if (room.IsOpen == false)
                return null;

            IPlayer player = user != null ? user : guest;
            if (await _roomRepository.TryAddUserToRoomAsync(roomCode, player) == false)
                return null;
        

            await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
            var playersInLobby = await _roomRepository.GetPlayersInLobbyAsync(roomCode);

            await Clients.Group(roomCode).SendAsync("PlayerJoined", playersInLobby);
            return room.Adapt<RoomDto>();

        }

        public async Task ChangePlayerReadyState(string roomCode, string username)
        {
            await Clients.Group(roomCode).SendAsync("OnPlayerChangedReadyState", username);
        }



    }
}

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
        private readonly IQuizRepository _quizRepository;
        private readonly IUserRepository _userRepository;
        public GameHub(IRoomRepository roomRepository, IRoomService roomService, IQuizRepository quizRepository, IUserRepository userRepository) 
        {
            _roomRepository = roomRepository;
            _roomService = roomService;
            _quizRepository = quizRepository;
            _userRepository = userRepository;

        }
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Player: {Context.ConnectionId}");

            var openRooms = await _roomRepository.GetOpenRoomsDtoAsync();

            await Clients.Caller.SendAsync("OpenRooms", openRooms.OrderBy(r=>r.CreatedAt));
        }
        public async override Task<Task> OnDisconnectedAsync(Exception? exception)
        {

            var room = await _roomRepository.RemovePlayerFromRoomAsync(Context.ConnectionId);
            if (room != null)
            {
                await Clients.Group(room.RoomCode).SendAsync("OnPlayerDisconnect", Context.ConnectionId);
                if (room.IsOpen == false)
                    await Clients.Group(room.RoomCode).SendAsync("OnRoomClosed", true);
                else
                    await Clients.Group(room.RoomCode).SendAsync("OnHostChange", room.Adapt<RoomDto>());
            }

            return base.OnDisconnectedAsync(exception);
        }


        public async Task<RoomDto?> OpenRoom(string roomCode, UserDto user = null!, Guest guest = null!)
        {
            var room = await _roomRepository.GetRoomByCodeAsync(roomCode);
            if (room == null)
                return null;

            if (room.IsOpen == true)
                return null;

            if (room.IsOpen == false && room.HostConnectionId != null)
                return null;

            IPlayer player = user != null ? user : guest;

            await _roomRepository.TryAddUserToRoomAsync(roomCode, player);
            room = await _roomRepository.SetRoomToOpen(roomCode,player.ConnectionId);
            

            await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);

            var playersInLobby = await _roomRepository.GetPlayersInLobbyAsync(roomCode);
            
            await Clients.Group(roomCode).SendAsync("PlayerJoined", playersInLobby);

            var openRooms = await _roomRepository.GetOpenRoomsDtoAsync();
            await Clients.All.SendAsync("OpenRooms", openRooms.OrderBy(r => r.CreatedAt));

            room.HostConnectionId = Context.ConnectionId;
            var roomDto = room.Adapt<RoomDto>();
            roomDto.Quiz = await _quizRepository.GetQuizDto(roomDto.Quiz.Id);
            return roomDto;
        }

        public async Task<RoomDto?> JoinRoom(string roomCode, UserDto user = null!, Guest guest = null!)
        {
            var room = await _roomRepository.GetRoomByCodeAsync(roomCode);
            if (room == null)
                return null;

            if (room.IsOpen == false)
                return null;

            if(user != null)
            {
                if (await TryAssignNewConnectionIdToExistingUser(roomCode, user.Id) == false)
                {
                    return null;
                }

            }

            else
            {
                IPlayer player = user != null ? user : guest;

                if (await _roomRepository.TryAddUserToRoomAsync(roomCode, player) == false)
                    return null;


                
                
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
            var playersInLobby = await _roomRepository.GetPlayersInLobbyAsync(roomCode);
            await Clients.Group(roomCode).SendAsync("PlayerJoined", playersInLobby);
            var roomDto = room.Adapt<RoomDto>();
            roomDto.Quiz = await _quizRepository.GetQuizDto(roomDto.Quiz.Id);
            return roomDto;

        }

        public async Task ChangePlayerReadyState(string roomCode, string username)
        {
            await Clients.Group(roomCode).SendAsync("OnPlayerChangedReadyState", username);
        }
        
        public async Task StartGame(string roomCode, bool val)
        {
            await Clients.Group(roomCode).SendAsync("OnGameStarted", val);
        }

        public async Task ChangePoints(string roomCode, string connectionId, int points)
        {
            try
            {
                await Clients.Group(roomCode).SendAsync("OnPointsChanged", connectionId, points);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public async Task<bool> TryAssignNewConnectionIdToExistingUser(string roomCode, int userId)
        {
            if(await _roomRepository.TryChangeUserConnectionId(roomCode, Context.ConnectionId, userId) == true)
            {
                return true;
            }
            return false;

        }



    }
}

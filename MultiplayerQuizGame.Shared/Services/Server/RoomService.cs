using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.Components;
using MultiplayerQuizGame.Shared.Models;
using MultiplayerQuizGame.Shared.Repositories.Interfaces;
using MultiplayerQuizGame.Shared.Services.Interfaces;
using System.Text;


namespace MultiplayerQuizGame.Shared.Services.Server
{
        public class RoomService : IRoomService
        {
            private const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            private const int ROOMCODE_LENGTH = 5;
            

            private Random _random;
            private readonly IRoomRepository _roomRepository;
            private readonly IQuizRepository _quizRepository;
            private readonly IUserRepository _userRepository;


            
            public RoomService(IRoomRepository roomRepository, IQuizRepository quizRepository, IUserRepository userRepository)
            {
                _random = new Random();
                _roomRepository = roomRepository;
                _quizRepository = quizRepository;
                _userRepository = userRepository;
            }
            public Task<Room> CloseRoom(string roomCode)
            {
                throw new NotImplementedException();
            }

       

            public async Task<Room> CreateRoom(int quizId)
            {
                string roomCode = await GenerateRoomCode();
                Room room = new Room
                {
                    RoomCode = roomCode,
                    Quiz = await _quizRepository.GetQuiz(quizId),
                    CreatedAt = DateTime.Now,
                    IsOpen = false,
                };

                if (await _roomRepository.AddRoomAsync(room).ConfigureAwait(false))
                {
                    return room;
                }
                return null!;
            }

            public async Task<string> GenerateRoomCode()
            {
                StringBuilder builder = new StringBuilder();
                do
                {
                    builder.Clear();
                    for (int i = 0; i < ROOMCODE_LENGTH; i++)
                    {
                        builder.Append(CHARS[_random.Next(CHARS.Length)]);
                    }
                }
                while (await IsCodeUnique(builder.ToString()) == null);
                return builder.ToString();
            }
            public async Task<Room> GetRoom(string roomCode)
            {
            throw new NotImplementedException();
            }
            

            public async Task<bool> IsCodeUnique(string roomCode)
            {
                if (roomCode == "0")
                    return false;

                var room = _roomRepository.GetRoomByCodeAsync(roomCode).Result;
                if (room != default(Room))
                {
                    if (room.IsOpen)
                        return false;
                    return true;
                }
                return true;
            }

        }
    }


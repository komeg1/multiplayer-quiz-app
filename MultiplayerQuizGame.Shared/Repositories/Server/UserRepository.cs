using Mapster;
using Microsoft.EntityFrameworkCore;
using MultiplayerQuizGame.Shared.Data;
using MultiplayerQuizGame.Shared.Enums;
using MultiplayerQuizGame.Shared.Models;
using MultiplayerQuizGame.Shared.Models.DTO;
using MultiplayerQuizGame.Shared.Repositories.Interfaces;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Buffers.Text;
using System.Net.NetworkInformation;


namespace MultiplayerQuizGame.Shared.Repositories.Server
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IQuizRepository _quizRepository;
        public UserRepository(DataContext context, IQuizRepository quizRepository)
        {
            _context = context;
            _quizRepository = quizRepository;
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.User.
                Where(u => u.Id == id).
                FirstOrDefaultAsync();

            return user;
        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var user = await _context.User.
                Where(u => u.Username == username).
                FirstOrDefaultAsync();

            return user;
        }
        public async Task AddUser(User user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task<UserQuizStampDto> SaveQuizStamp(UserQuizStampDto stampDto)
        {
            UserQuizStamp stamp = new UserQuizStamp
            {
                Quiz = await _quizRepository.GetQuizAsync(stampDto.Quiz.Id),
                User = await GetUserByIdAsync(stampDto.UserId),
                Points = 0,
                Score = 0,
                DateTime = DateTime.Now,
                GameMode = stampDto.GameMode,
            };
            var savedStamp = await _context.UserQuizStamp.AddAsync(stamp);
            await _context.SaveChangesAsync();

            UserQuizStampDto dto = savedStamp.Entity.Adapt<UserQuizStampDto>();
            dto.Quiz.QuestionCount = stamp.Quiz.Questions.Count;
            return savedStamp.Entity.Adapt<UserQuizStampDto>();
        }

        public async Task UpdateStamp(int stampId, int points, int score, int experience)
        {
            var stamp = _context.UserQuizStamp.
                Include(s => s.User).
                FirstOrDefault(s => s.Id == stampId);

            //Eventually move to separate method call
            try
            {
                await UpdateExperience(stamp!.User.Id, experience);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            if (stamp != null)
            {
                stamp.Points = points;
                stamp.Score = score;
                stamp.Experience = experience;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserQuizStampDto>> GetUserGameHistory(int userId)
        {
            var result = await _context.UserQuizStamp.
                Include(s => s.Quiz).
                ThenInclude(q=>q.Questions).
                Where(s => s.User.Id == userId).
                AsSplitQuery().
                ToListAsync();

            List<UserQuizStampDto> dtos = new List<UserQuizStampDto>();
            UserQuizStampDto temp;
            foreach (var entry in result)
            {
                temp = entry.Adapt<UserQuizStampDto>();
                temp.Quiz.QuestionCount = entry.Quiz.Questions.Count;
                dtos.Add(temp);
            }

            return dtos;
        }

        public Task<UserDto> GetLoggedUser()
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> GetUserDtoByIdAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            if (user != null)
            {
                return user.Adapt<UserDto>();
            }
            return null!;
        }
        public async Task<UserDto> UpdateAvatar(int userId, byte[] avatarBytes)
        {
            var user = await GetUserByIdAsync(userId);
            user.AvatarB64 = "data:image / png; base64," + Convert.ToBase64String(avatarBytes);
            await _context.SaveChangesAsync();

            return user.Adapt<UserDto>();
        }

        public async Task UpdateExperience(int userId, int experience)
        {
            var user = await GetUserByIdAsync(userId);
            user.Experience += experience;

            if(user.Experience/100 > user.Level)
                user.Level = user.Experience/100;

            await _context.SaveChangesAsync();


        }
        public async Task<string> GetUserAvatar(int id)
        {
            var user = await GetUserByIdAsync(id);

            return user.AvatarB64;
        }
    }


}

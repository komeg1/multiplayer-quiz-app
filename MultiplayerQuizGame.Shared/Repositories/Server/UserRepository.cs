using Mapster;
using Microsoft.EntityFrameworkCore;
using MultiplayerQuizGame.Shared.Data;
using MultiplayerQuizGame.Shared.Enums;
using MultiplayerQuizGame.Shared.Models;
using MultiplayerQuizGame.Shared.Models.DTO;
using MultiplayerQuizGame.Shared.Repositories.Interfaces;


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

            UserQuizStampDto dto= savedStamp.Entity.Adapt<UserQuizStampDto>();
            dto.Quiz.QuestionCount = stamp.Quiz.Questions.Count;
            return savedStamp.Entity.Adapt<UserQuizStampDto>();
        }

        public async Task UpdateStampPoints(int stampId, int points, int score)
        {
            var stamp = _context.UserQuizStamp.
                FirstOrDefault(s=>s.Id == stampId);

            if(stamp != null)
            {
                stamp.Points = points;
                stamp.Score = score;
            }
            await _context.SaveChangesAsync();
        }
        public async Task<List<UserQuizStampDto>> GetUserGameHistory(int userId)
        {
            var result = await _context.UserQuizStamp.
                Include(s=>s.Quiz).
                Where(s => s.User.Id == userId).
                ToListAsync();

            List<UserQuizStampDto> dtos = new List<UserQuizStampDto>();
            UserQuizStampDto temp;
            foreach (var entry in result )
            {
                temp = entry.Adapt<UserQuizStampDto>();
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
            if(user!= null)
            {
                return user.Adapt<UserDto>();
            }
            return null!;
        }
    }


}

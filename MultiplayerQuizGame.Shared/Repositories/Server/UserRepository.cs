using Mapster;
using Microsoft.EntityFrameworkCore;
using MultiplayerQuizGame.Shared.Data;
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
        public async Task<User> GetUserAsync(int id)
        {
            var user = await _context.User.
                Where(u => u.Id == id).
                FirstOrDefaultAsync();

            return user;
        }
        public async Task<User> GetUserAsync(string username)
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

        public async Task<UserQuizStampDto> SaveQuizStamp(int quizId, int userId)
        {
            UserQuizStamp stamp = new UserQuizStamp
            {
                Quiz = await _quizRepository.GetQuiz(quizId),
                User = await GetUserAsync(userId),
                Points = 0,
                DateTime = DateTime.Now,
            };

            var savedStamp = await _context.UserQuizStamp.AddAsync(stamp);
            await _context.SaveChangesAsync();

            UserQuizStampDto stampDto = new UserQuizStampDto
            {
                Id = savedStamp.Entity.Id,
                QuizId = quizId,
                UserId = userId,
                DateTime = savedStamp.Entity.DateTime,
                Points = 0,
            };


            return stampDto;
        }

        public async Task UpdateStampPoints(int stampId, int points)
        {
            var stamp = _context.UserQuizStamp.
                FirstOrDefault(s=>s.Id == stampId);

            if(stamp != null)
            {
                stamp.Points = points;
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
                temp.QuizId = entry.Quiz.Id;
                dtos.Add(temp);
            }

            return dtos;
        }

        public Task<UserDto> GetLoggedUser()
        {
            throw new NotImplementedException();
        }
    }


}

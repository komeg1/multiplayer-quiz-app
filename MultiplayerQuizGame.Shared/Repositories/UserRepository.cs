using Microsoft.EntityFrameworkCore;
using MultiplayerQuizGame.Shared.Data;
using MultiplayerQuizGame.Shared.Models;
using MultiplayerQuizGame.Shared.Models.DTO;
using MultiplayerQuizGame.Shared.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Repositories
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
        public async Task<User> GetUser(int id)
        {
            var user = await _context.User.
                Where(u => u.Id == id).
                FirstOrDefaultAsync();

            return user;
        }
        public async Task<User> GetUser(string username)
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
            if (stampDto.Id != 0)
            {
                var entity = _context.UserQuizStamp.FirstOrDefault(s => s.Id == stampDto.Id);

                if (entity != null)
                {
                    entity.Points = stampDto.Points;
                }

                _context.SaveChanges();

                return stampDto;
            }
            else
            {

                UserQuizStamp stamp = new UserQuizStamp
                {
                    Quiz = await _quizRepository.GetQuiz(stampDto.QuizId),
                    User = await GetUser(stampDto.UserId),
                    Points = stampDto.Points,
                    DateTime = DateTime.Now,
                };

                await _context.UserQuizStamp.AddAsync(stamp);
                await _context.SaveChangesAsync();

                stampDto.DateTime = stamp.DateTime;
                stampDto.Points = 0;

                return stampDto;
            }
        }

        
    }
}

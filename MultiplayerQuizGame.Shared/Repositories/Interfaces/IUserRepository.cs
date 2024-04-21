using MultiplayerQuizGame.Shared.Models;
using MultiplayerQuizGame.Shared.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDto> GetLoggedUser();
        Task<User> GetUserAsync(int id);
        Task<User> GetUserAsync(string username); 
        Task AddUser(User user);
        Task<UserQuizStampDto> SaveQuizStamp(int quizId, int userId = 0);
        Task UpdateStampPoints(int stampId, int points);
        Task<List<UserQuizStampDto>> GetUserGameHistory(int id);

    }
}

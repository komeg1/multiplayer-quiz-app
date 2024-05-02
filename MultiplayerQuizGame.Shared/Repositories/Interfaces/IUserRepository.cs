using MultiplayerQuizGame.Shared.Enums;
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
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<UserDto> GetUserDtoByIdAsync(int id);
        Task AddUser(User user);
        Task<UserQuizStampDto> SaveQuizStamp(UserQuizStampDto stampDto);
        Task UpdateStamp(int stampId, int points, int score, int experience);
        Task<List<UserQuizStampDto>> GetUserGameHistory(int id);

        Task UpdateExperience(int userId, int experience);
    }
}

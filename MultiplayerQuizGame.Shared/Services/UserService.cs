using Microsoft.EntityFrameworkCore;
using MultiplayerQuizGame.Shared.Data;
using MultiplayerQuizGame.Shared.Models;
using MultiplayerQuizGame.Shared.Repositories.Interfaces;
using MultiplayerQuizGame.Shared.Services.Interfaces;
using MultiplayerQuizGame.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _pepper = "BB3#$ak*";
        private readonly int _iteration = 3;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Register(Credentials registerCredentials)
        {
            User user = await _userRepository.GetUser(registerCredentials.Username);

            if (user != default)
                return new User { Username = string.Empty };

            user = new User
            {
                Username = registerCredentials.Username,
                PasswordSalt = PasswordHasher.GenerateSalt()
            };
            user.PasswordHash = PasswordHasher.ComputeHash(registerCredentials.Password, user.PasswordSalt, _pepper, _iteration);
            await _userRepository.AddUser(user);
            

            return user;
        }

        public async Task<User> Login(Credentials loginCredentials)
        {
            var user = await _userRepository.GetUser(loginCredentials.Username);

            if (user == null)
                return null!;

            var passwordHash = PasswordHasher.ComputeHash(loginCredentials.Password, user.PasswordSalt, _pepper, _iteration);
            if (user.PasswordHash != passwordHash)
                return null!;

            return user;
        }



    }
}

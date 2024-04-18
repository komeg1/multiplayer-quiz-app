using MultiplayerQuizGame.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> Register(Credentials registerCredentials);
        Task<User> Login(Credentials loginCredentials);
        

    }
}

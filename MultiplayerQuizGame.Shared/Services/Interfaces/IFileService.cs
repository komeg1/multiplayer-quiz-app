using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
namespace MultiplayerQuizGame.Shared.Services.Interfaces
{
    public interface IFileService
    { 
        Task<string> SaveUserAvatarAsync( IBrowserFile file, int userId);
        Task<string> DeleteFile(string containerName, string filePath);
        Task<string> GetUserAvatarAsync(int userId);

    }
}

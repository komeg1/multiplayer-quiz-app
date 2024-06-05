using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using MultiplayerQuizGame.Shared.Models;
using MultiplayerQuizGame.Shared.Repositories.Interfaces;
using MultiplayerQuizGame.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Services.Server
{
    public class FileService : IFileService
    {
        private const string CONTAINER_NAME = "profileavatars";
        
        private const string DEFAULT_AVATAR_NAME = "default.jpg";
        private const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private const int FILENAME_LENGTH = 15;

        private static readonly Random _random = new Random();
        private readonly IUserRepository _userRepository;
        private string _connectionString;

        public FileService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _connectionString = configuration["ConnectionStrings:BlobServiceConnectionString"];
        }
        public Task<string> DeleteFile(string containerName, string filePath)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SaveUserAvatarAsync(IBrowserFile file, int userId = 0)
        {
            BlobContainerClient blobContainerClient = GetBlobContrainer(CONTAINER_NAME);
            
            var userDto = await _userRepository.GetUserDtoByIdAsync(userId);

           if(blobContainerClient.GetBlobClient(userDto.AzureAvatarName) != null && userDto.AzureAvatarName != DEFAULT_AVATAR_NAME)
            {
               await blobContainerClient.DeleteBlobAsync(userDto.AzureAvatarName);
            }

            var avatarRandomName = GetRandomString(FILENAME_LENGTH);
            await blobContainerClient.UploadBlobAsync(avatarRandomName,file.OpenReadStream());

            await _userRepository.UpdateAvatarName(userId,avatarRandomName);

            return avatarRandomName;
        }

        

        public BlobContainerClient GetBlobContrainer(string containerName)
        {
            BlobContainerClient blobContainerClient = new BlobContainerClient(_connectionString, containerName);
            blobContainerClient.CreateIfNotExists();

            return blobContainerClient;
        }

        public async Task<string> GetUserAvatarAsync(int userId)
        {
            BlobContainerClient blobContainerClient = GetBlobContrainer(CONTAINER_NAME);
            BlobClient blobClient = blobContainerClient.GetBlobClient($"{userId}_avatar");

            if (await blobClient.ExistsAsync())
            {
                var downloadInfo = await blobClient.DownloadAsync();
                using (var memoryStream = new MemoryStream())
                {
                    await downloadInfo.Value.Content.CopyToAsync(memoryStream);
                    byte[] byteArray = memoryStream.ToArray();
                    return Convert.ToBase64String(byteArray);
                }
            }
            else
            {
                blobClient = blobContainerClient.GetBlobClient("default");

                var downloadInfo = await blobClient.DownloadAsync();
                using (var memoryStream = new MemoryStream())
                {
                    await downloadInfo.Value.Content.CopyToAsync(memoryStream);
                    byte[] byteArray = memoryStream.ToArray();
                    return Convert.ToBase64String(byteArray);
                }
            }


            
        }

        public static string GetRandomString(int length)
        {
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(CHARS[_random.Next(CHARS.Length)]);
            }
            return result.ToString();
        }



    }
}

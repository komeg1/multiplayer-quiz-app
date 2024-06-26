using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MultiplayerQuizGame.Shared.Models.Interfaces;

namespace MultiplayerQuizGame.Shared.Models
{
    public class User : IPlayer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string AzureAvatarName { get; set; }
        public List<Room>? Rooms { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }

        //Used in lobbies for actions like IsReady..
        public string? ConnectionId { get; set; }

    }
}
﻿using MultiplayerQuizGame.Shared.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Models
{
    public class PlayersInLobby
    {
        public List<UserDto> Users { get; set; } = new();
        public List<Guest> Guests { get; set; } = new();
    }
}

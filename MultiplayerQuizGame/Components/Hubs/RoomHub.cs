using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MultiplayerQuizGame.Components.Data;
using MultiplayerQuizGame.Components.Models;

namespace MultiplayerQuizGame.Components.Hubs
{
    public class RoomHub : Hub
    {
        public async Task JoinRoom(UserConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.RoomCode);
            await Clients.Group(userConnection.RoomCode)
                            .SendAsync("ReceiveMessage","admin",$"{userConnection.Username} has joined {userConnection.RoomCode}");

        }
    }  
}
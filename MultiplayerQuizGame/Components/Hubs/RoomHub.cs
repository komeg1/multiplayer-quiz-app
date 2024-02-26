using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace MultiplayerQuizGame.Components.Hubs
{
    public class RoomHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public Task JoinRoom(string roomCode)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId,roomCode);
        }

        public Task SendMessageToRoom(string roomCode, string message)
        {
            return Clients.Group(roomCode).SendAsync("ReceiveGroupMessage",message);
        }
    }
}
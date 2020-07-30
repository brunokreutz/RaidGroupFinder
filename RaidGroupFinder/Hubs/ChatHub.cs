using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RaidGroupFinder.Data;

namespace BlazorSignalRApp.Server.Hubs
{
    public class ChatHub : Hub
    {
        //public async Task SendMessage(string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", message);
        //}

        public async Task SendMessage(string user, string message, string room, bool join)
        {
            if (join)
            {
                var splits = user.Split(" |");
                await JoinRoom(room).ConfigureAwait(false);
                await Clients.Group(room).SendAsync("ReceiveMessage", "BOT", $"{splits.First()}, trainer code:{splits.Last()} joined the raid room.", DateTime.Now).ConfigureAwait(true);
            }
            else
            {
                await Clients.Group(room).SendAsync("ReceiveMessage", user, message, DateTime.Now).ConfigureAwait(true);
            }
        }

        public Task JoinRoom(string roomName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }
    }
}
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

        private ChatHistoryRepository chatHistoryRepository;

        public ChatHub(ChatHistoryRepository chatHistoryRepository)
        {
            this.chatHistoryRepository = chatHistoryRepository;
        }

        public async Task SendMessage(string user, string message, Guid room, bool join)
        {
            var chat = new ChatHistory() { Guid = Guid.NewGuid(), Date = DateTime.Now, Group = room, Message = message, User = user };
            if (join)
            {
                var splits = user.Split(" |");
                await JoinRoom(room.ToString()).ConfigureAwait(false);
                chat.User = "BOT";
                chat.Message = $"{splits.First()}, trainer code:{splits.Last()} joined the raid room.";
                await Clients.Group(room.ToString()).SendAsync("ReceiveMessage", chat).ConfigureAwait(true);
            }
            else
            {
                await Clients.Group(room.ToString()).SendAsync("ReceiveMessage", chat).ConfigureAwait(true);
            }
            await chatHistoryRepository.SaveMessage(chat);
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
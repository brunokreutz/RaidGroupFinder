using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RaidGroupFinder.Data;
using RaidGroupFinder.Data.Model;

namespace BlazorSignalRApp.Server.Hubs
{
    public class ChatHub : Hub
    {
        public const string BOT = null;

        private readonly DbService ChatDbService;

        public ChatHub(DbService chatDbService)
        {
            this.ChatDbService = chatDbService;
        }

        public async Task SendMessage(string user, string message, Guid room, string id, bool finish)
        {
            var chat = new ChatHistory() { Guid = Guid.NewGuid(), Date = DateTime.UtcNow, Group = room, Message = message, User = user };
            if (finish)
            {
                var splits = user.Split(" |");
                string name = splits.First();
                chat.User = BOT;
                chat.Message = $"{name} finished the raid.";
            }
            else if (id != null)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, room.ToString()).ConfigureAwait(false);

                var con = new Connection() { Active = true, ConnectionID = Context.ConnectionId, Room = room, UserId = id };
                await ChatDbService.AddConnection(con);

                var splits = user.Split(" |");
                string name = splits.First();
                Context.Items.Add(Context.ConnectionId, (name, room));

                chat.User = BOT;
                chat.Message = $"{name} joined.";

            }
            await Clients.Group(room.ToString()).SendAsync("ReceiveMessage", chat).ConfigureAwait(true);
            await ChatDbService.SaveMessage(chat);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var (name, guid) = ((string name, Guid guid))Context.Items[Context.ConnectionId];
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, guid.ToString());

                var chat = new ChatHistory()
                {
                    Guid = Guid.NewGuid(),
                    Date = DateTime.UtcNow,
                    Group = guid,
                    Message = $"{name} left.",
                    User = BOT
                };

                await Clients.Group(guid.ToString()).SendAsync("ReceiveMessage", chat).ConfigureAwait(true);
                await ChatDbService.SaveMessage(chat);
                await ChatDbService.DeactivateConnection(Context.ConnectionId);
                await base.OnDisconnectedAsync(exception);
            }
            catch
            {

            }
        }
    }
}
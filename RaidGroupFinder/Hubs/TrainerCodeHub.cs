using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace TrainerCodeHubGroupFinder.Hubs
{
    public class TrainerCodeHub : Hub
    {

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}

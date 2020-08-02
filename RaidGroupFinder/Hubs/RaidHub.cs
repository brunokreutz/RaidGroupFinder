using Microsoft.AspNetCore.SignalR;
using RaidGroupFinder.Data;
using RaidGroupFinder.Data.Model;
using System.Threading.Tasks;

namespace RaidGroupFinder.Hubs
{
    public class RaidHub : Hub
    {
        private readonly DbService ChatDbService;

        public RaidHub(DbService chatDbService)
        {
            this.ChatDbService = chatDbService;
        }

        public async Task SendMessage(RaidBattle raidBattle)
        {
            await Clients.All.SendAsync("ReceiveMessage", raidBattle);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RaidGroupFinder.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaidGroupFinder.Data
{
    public class DbService
    {
        private ApplicationDbContext context;
        public DbService(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }

        public async Task SaveMessage(ChatHistory chatHistory)
        {
            await context.AddAsync(chatHistory);
            await context.SaveChangesAsync();
        }

        public async Task<List<ChatHistory>> GetGroupHistory(Guid group)
        {
            return await context.ChatHistories.Where(p => p.Group == group).OrderBy(p=> p.Date).ToListAsync();
        }

        public async Task AddConnection(Connection connection)
        {
            await context.AddAsync(connection);
            await context.SaveChangesAsync();
        }

        public async Task DeactivateConnection(string connectionId)
        {
            var con = await context.Connections.FirstOrDefaultAsync(p => p.Active && p.ConnectionID == connectionId);
            con.Active = false;
            context.Update(con);
            await context.SaveChangesAsync();
        }

        public async Task<List<Raid>> GetActiveRaids()
        {
            return await context.Raids.Include(p => p.Pokemon).Where(p => p.Available).ToListAsync();
        }

        public async Task CreateRaidBattle(RaidBattle raidBattle)
        {
            await context.AddAsync(raidBattle);
            await context.SaveChangesAsync();
        }

        public async Task<List<RaidBattle>> GetActiveRaidBattles()
        {
            return await context.RaidBattles.Include(p=> p.Raid).Include(p => p.Raid.Pokemon).Where(p => p.Hatched > DateTime.UtcNow.AddMinutes(-45)).OrderByDescending(p => p.Created).ToListAsync();
        }
        public async Task<List<RaidBattle>> GetLastInactiveRaidBattles()
        {
            return await context.RaidBattles.Include(p => p.Raid).Include(p => p.Raid.Pokemon).Where(p => p.Hatched < DateTime.UtcNow.AddMinutes(-45)).OrderByDescending(p => p.Created).Take(5).ToListAsync();
        }

        public async Task<int> GetPlayersInRaidRoom(Guid guid)
        {
            return await context.Connections.Where(p => p.Active && p.Room == guid).CountAsync();
        }

    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaidGroupFinder.Data
{
    public class ChatHistoryRepository
    {
        private ApplicationDbContext context;
        public ChatHistoryRepository(ApplicationDbContext applicationDbContext)
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
            return await context.ChatHistories.Where(p => p.Group == group).ToListAsync();
        }

    }
}

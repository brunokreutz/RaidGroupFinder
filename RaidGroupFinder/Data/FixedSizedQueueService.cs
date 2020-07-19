using System;
using System.Linq;
using System.Threading.Tasks;

namespace RaidGroupFinder.Data
{
    public class FixedSizedQueueService
    {
        private FixedSizedQueue<string> FixedSizedQueue;
        public FixedSizedQueueService()
        {
            FixedSizedQueue = new FixedSizedQueue<string>(15);
        }

        public Task<FixedSizedQueue<string>> GetQueue()
        {
            return Task.FromResult(FixedSizedQueue);
        }

        public Task Enqueue(string message)
        {
            FixedSizedQueue.Enqueue(message);
            return Task.CompletedTask;
        }
    }
}

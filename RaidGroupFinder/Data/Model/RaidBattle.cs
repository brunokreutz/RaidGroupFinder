using System;

namespace RaidGroupFinder.Data.Model
{
    public class RaidBattle
    {
        public Guid Guid { get; set; }
        public virtual Raid Raid { get; set; }
        public DateTime Hatched { get; set; }
        public DateTime Created { get; set; }
        public string Location { get; set; }
        public string Host { get; set; }
        public string HostUserId { get; set; }
        public bool Active { get; set; }
    }
}

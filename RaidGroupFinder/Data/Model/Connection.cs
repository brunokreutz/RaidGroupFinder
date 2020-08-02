using System;

namespace RaidGroupFinder.Data.Model
{
    public class Connection
    {
        public int Id { get; set; }
        public string ConnectionID { get; set; }
        public string UserId { get; set; }
        public Guid Room { get; set; }
        public bool Active { get; set; }

        public Connection()
        {
        }
    }
}

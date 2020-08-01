using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaidGroupFinder.Data.Model
{
    public class Connection
    {
        public int Id { get; set; }
        public string ConnectionID { get; set; }
        public string UserId { get; set; }
        public Guid Room {get;set;}
        public bool Active { get; set; }

        public Connection()
        {
        }
    }
}

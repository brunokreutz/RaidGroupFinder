using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaidGroupFinder.Data
{
    public class ChatHistory
    {
        public Guid Guid { get; set; }
        public string User { get; set; }
        public string Message { get; set; }
        public Guid Group { get; set; }
        public DateTime Date { get; set; }

    }
}

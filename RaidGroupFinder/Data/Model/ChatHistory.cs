using System;

namespace RaidGroupFinder.Data
{
    public class ChatHistory
    {
        public Guid Guid { get; set; }
        public string User { get; set; }
        public string Message { get; set; }
        public Guid Group { get; set; }
        public DateTimeOffset Date { get; set; }

        public ChatHistory()
        {
        }
    }
}

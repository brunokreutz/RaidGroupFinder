namespace RaidGroupFinder.Data.Model
{
    public class Raid
    {
        public short Id { get; set; }
        public virtual Pokemon Pokemon { get; set; }
        public byte Tier { get; set; }
        public bool Available { get; set; }
    }
}

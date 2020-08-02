using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaidGroupFinder.Data.Model;

namespace RaidGroupFinder.Data.Configuration
{
    public class RaidConfiguration : IEntityTypeConfiguration<Raid>
    {
        public void Configure(EntityTypeBuilder<Raid> builder)
        {
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.Tier).IsRequired();
            builder.Property(p => p.Available).IsRequired();
            builder.HasIndex(p => p.Available);
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Pokemon);

        }
    }
}

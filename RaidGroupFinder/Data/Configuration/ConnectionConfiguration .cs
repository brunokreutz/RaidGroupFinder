using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaidGroupFinder.Data.Model;

namespace RaidGroupFinder.Data.Configuration
{
    public class ConnectionConfiguration : IEntityTypeConfiguration<Connection>
    {
        public void Configure(EntityTypeBuilder<Connection> builder)
        {
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.Room).IsRequired();
            builder.Property(p => p.Active).IsRequired();
            builder.Property(p => p.ConnectionID).IsRequired();
            builder.Property(p => p.UserId).IsRequired().HasMaxLength(450);
            builder.HasIndex(p => p.Room);
            builder.HasIndex(p => p.Active);
            builder.HasKey(p => p.Id);
        }
    }
}

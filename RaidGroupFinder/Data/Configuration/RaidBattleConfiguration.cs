using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaidGroupFinder.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaidGroupFinder.Data.Configuration
{
    public class RaidBattleConfiguration : IEntityTypeConfiguration<RaidBattle>
    {
        public void Configure(EntityTypeBuilder<RaidBattle> builder)
        {
            builder.Property(p => p.Guid).IsRequired();
            builder.Property(p => p.Hatched).IsRequired();
            builder.Property(p => p.Host).IsRequired();
            builder.Property(p => p.Created).IsRequired();
            builder.Property(p => p.Active).IsRequired();
            builder.Property(p => p.Location).IsRequired().HasMaxLength(50);

            builder.HasIndex(p => p.Hatched);
            builder.HasIndex(p => p.Created);
            builder.HasIndex(p => p.Active);

            builder.HasKey(p => p.Guid);
            builder.HasOne(p => p.Raid);

        }
    }
}

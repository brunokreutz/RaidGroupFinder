using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RaidGroupFinder.Data.Configuration
{
    public class ChatHistoryConfiguration : IEntityTypeConfiguration<ChatHistory>
    {
        public void Configure(EntityTypeBuilder<ChatHistory> builder)
        {
            builder.HasKey(p => p.Guid);
            builder.Property(p => p.Group).IsRequired();
            builder.Property(p => p.Date).IsRequired();
            builder.Property(p => p.User).HasMaxLength(32);
            builder.HasIndex(p => p.Group);
            builder.HasIndex(p => p.Date);
        }
    }
}

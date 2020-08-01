using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RaidGroupFinder.Data.Configuration;
using RaidGroupFinder.Data.Model;

namespace RaidGroupFinder.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ChatHistory> ChatHistories { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Raid> Raids { get; set; }
        public DbSet<RaidBattle> RaidBattles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ChatHistoryConfiguration());
            builder.ApplyConfiguration(new ConnectionConfiguration());
            builder.ApplyConfiguration(new PokemonConfiguration());
            builder.ApplyConfiguration(new RaidConfiguration());
            builder.ApplyConfiguration(new RaidBattleConfiguration());
            
            base.OnModelCreating(builder);
        }
    }
}

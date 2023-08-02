using LagoaTrading.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LagoaTrading.Data.Repository.Contexts
{
    public class LagoaTradingContext : DbContext
    {
        public DbSet<Candlestick> Candlestick { get; set; }
        public DbSet<Circuit> Circuit { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Invite> Invite { get; set; }
        public DbSet<Market> Market { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<Parameter> Parameter { get; set; }
        public DbSet<SyncControl> SyncControl { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserAccount> UserAccount { get; set; }

        public LagoaTradingContext(DbContextOptions options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

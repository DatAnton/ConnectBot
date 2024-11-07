using ConnectBot.Domain.Entities;
using ConnectBot.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ConnectBot.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new TeamColorConfiguration().Configure(modelBuilder.Entity<TeamColor>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TeamColor> TeamColors { get; set; }
    }
}

using ConnectBot.Domain.Entities;
using ConnectBot.Domain.Interfaces;
using ConnectBot.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ConnectBot.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new TeamColorConfiguration().Configure(modelBuilder.Entity<TeamColor>());
            new UserConfiguration().Configure(modelBuilder.Entity<User>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TeamColor> TeamColors { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<CommunicationRequest> CommunicationRequests { get; set; }
        public DbSet<EventParticipation> EventParticipations { get; set; }
    }
}

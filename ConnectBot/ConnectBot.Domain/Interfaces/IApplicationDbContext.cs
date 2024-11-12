using ConnectBot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConnectBot.Domain.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TeamColor> TeamColors { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<CommunicationRequest> CommunicationRequests { get; set; }
        public DbSet<EventParticipation> EventParticipations { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

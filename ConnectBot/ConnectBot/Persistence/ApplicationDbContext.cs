using ConnectBot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConnectBot.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        public DbSet<User> Users { get; set; }
    }
}

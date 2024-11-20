using ConnectBot.Domain.Entities;
using ConnectBot.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConnectBot.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IDbContextFactory _dbContextFactory;

        public UserService(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<User?> GetUserByChatId(long id, CancellationToken cancellationToken)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.ChatId == id, cancellationToken);
            return user;
        }

        public async Task<List<User>> GetUserAdmins(CancellationToken cancellationToken)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            var admins = await dbContext.Users.Where(u => u.IsAdmin).ToListAsync(cancellationToken);
            return admins;
        }
    }
}

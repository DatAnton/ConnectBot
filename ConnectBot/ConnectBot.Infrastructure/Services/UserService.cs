using ConnectBot.Domain.Entities;
using ConnectBot.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConnectBot.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _context;

        public UserService(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User> GetUserByChatId(long id, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.ChatId == id, cancellationToken);
            return user;
        }
    }
}

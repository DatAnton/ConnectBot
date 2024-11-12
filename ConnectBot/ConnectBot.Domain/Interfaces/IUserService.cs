using ConnectBot.Domain.Entities;

namespace ConnectBot.Domain.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByChatId(long id, CancellationToken cancellationToken);
    }
}

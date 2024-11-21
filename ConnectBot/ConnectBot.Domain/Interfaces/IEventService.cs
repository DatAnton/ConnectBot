using ConnectBot.Domain.Entities;

namespace ConnectBot.Domain.Interfaces
{
    public interface IEventService
    {
        Task<Event?> GetTodayEvent(DateTime startDateTime, CancellationToken cancellationToken);
        Task<List<TeamColor>> GetTeamColors(CancellationToken cancellationToken);
    }
}

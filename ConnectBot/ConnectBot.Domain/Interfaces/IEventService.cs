using ConnectBot.Domain.Entities;

namespace ConnectBot.Domain.Interfaces
{
    public interface IEventService
    {
        Task<Event?> GetTodayEvent(CancellationToken cancellationToken);
        Task<(int, TeamColor)> GetEventUniqueNumberAndTeam(User user, Event todayEvent, CancellationToken cancellationToken);
    }
}

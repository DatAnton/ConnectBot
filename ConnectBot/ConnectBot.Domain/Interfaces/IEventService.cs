using ConnectBot.Domain.Entities;

namespace ConnectBot.Domain.Interfaces
{
    public interface IEventService
    {
        Task<Event?> GetTodayEvent(DateTime eventDate, CancellationToken cancellationToken);
        Task<List<TeamColor>> GetTeamColors(CancellationToken cancellationToken);
        Task<List<EventBenefit>> GetEventBenefits(CancellationToken cancellationToken);
    }
}

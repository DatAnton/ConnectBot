using ConnectBot.Domain.Entities;
using ConnectBot.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConnectBot.Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly IApplicationDbContext _context;

        public EventService(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Event?> GetTodayEvent(CancellationToken cancellationToken)
        {
            var todayEvent =
                await _context.Events.FirstOrDefaultAsync(e => e.StartDateTime.Date == DateTime.UtcNow.Date,
                    cancellationToken);
            return todayEvent;
        }

        public async Task<(int, TeamColor)> GetEventUniqueNumberAndTeam(User user, Event todayEvent, CancellationToken cancellationToken)
        {
            var participationsCount =
                await _context.EventParticipations.CountAsync(ep => ep.EventId == todayEvent.Id, cancellationToken);

            var uniqueNumber = participationsCount + 1;

            var teamColorId = uniqueNumber % todayEvent.NumberOfTeams;

            var teamColor =
                await _context.TeamColors.FirstOrDefaultAsync(tc => tc.Id == teamColorId, cancellationToken);

            return (uniqueNumber, teamColor);
        }
    }
}

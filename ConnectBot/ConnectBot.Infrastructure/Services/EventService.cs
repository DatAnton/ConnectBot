using ConnectBot.Domain.Entities;
using ConnectBot.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConnectBot.Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly IDbContextFactory _dbContextFactory;

        public EventService(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<Event?> GetTodayEvent(CancellationToken cancellationToken)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            var todayEvent =
                await dbContext.Events.FirstOrDefaultAsync(e => e.StartDateTime.Date == DateTime.UtcNow.Date,
                    cancellationToken);
            return todayEvent;
        }

        public async Task<List<TeamColor>> GetTeamColors(CancellationToken cancellationToken)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            var teamColors =
                await dbContext.TeamColors.ToListAsync(cancellationToken);
            return teamColors;
        }
    }
}

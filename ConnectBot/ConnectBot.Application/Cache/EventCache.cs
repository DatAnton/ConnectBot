using ConnectBot.Domain.Entities;
using ConnectBot.Domain.Interfaces;

namespace ConnectBot.Application.Cache
{
    public class EventCache
    {
        private readonly IEventService _eventService;
        private Domain.Entities.Event? _todayEvent { get; set; }
        private Dictionary<int, TeamColor> _teamColors = new();
        private List<int> _eventParticipations = new();

        public EventCache(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<Domain.Entities.Event?> GetTodayEvent(CancellationToken cancellationToken)
        {
            if (_todayEvent == null || _todayEvent.StartDateTime.Date != DateTime.UtcNow.Date)
            {
                _todayEvent = await _eventService.GetTodayEvent(cancellationToken);
                _eventParticipations = _todayEvent != null
                    ? _todayEvent.EventParticipations.Select(ep => ep.UserId).ToList()
                    : new List<int>();
            }

            return _todayEvent;
        }

        public async Task<TeamColor> GetTeamColorById(int id, CancellationToken cancellationToken)
        {
            if (!_teamColors.Any())
            {
                var teamColors = await _eventService.GetTeamColors(cancellationToken);
                _teamColors = teamColors.ToDictionary(k => k.Id, ent => ent);
            }

            return _teamColors[id];
        }

        public int GetCurrentParticipationsCount => _eventParticipations.Count;

        public void AddParticipation(int userId)
        {
            _eventParticipations.Add(userId);
        }

        public bool IsOnEvent(int userId)
        {
            return _eventParticipations.Any(x => x == userId);
        }
    }
}

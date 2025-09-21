using ConnectBot.Domain.Entities;
using ConnectBot.Domain.Interfaces;
using Telegram.Bot.Types;

namespace ConnectBot.Application.Cache
{
    public class EventCache
    {
        private readonly IEventService _eventService;
        private Domain.Entities.Event? _todayEvent { get; set; }
        private Dictionary<int, TeamColor> _teamColors = new();
        private Dictionary<int, int?> _eventParticipations = new(); //key: userId, value: eventBenefitId
        private List<EventBenefit> _eventBenefits = new();
        private bool _iceBreakerGenerated = false;
        private List<EventBenefit> _availableEventBenefits = new();

        public EventCache(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<Domain.Entities.Event?> GetTodayEvent(CancellationToken cancellationToken)
        {
            var eventDate = DateTime.UtcNow.AddHours(2).Date; // hours for UTC+2
            if (_todayEvent == null || _todayEvent.StartDateTime.Date != eventDate)
            {
                _todayEvent = await _eventService.GetTodayEvent(eventDate, cancellationToken);
                _eventParticipations = _todayEvent != null
                    ? _todayEvent.EventParticipations.ToDictionary(ep => ep.UserId, ep => ep.EventBenefitId)
                    : new Dictionary<int, int?>();
                _iceBreakerGenerated = false;
                _eventBenefits = await _eventService.GetEventBenefits(cancellationToken);
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

        public List<EventBenefit> GetAvailableEventBenefits()
        {
            if (!_availableEventBenefits.Any())
            {
                _availableEventBenefits = _eventParticipations.Values.Any(v => v.HasValue)
                    ? _eventBenefits.Where(eb => !eb.IsOneTimeBenefit).ToList()
                : _eventBenefits;

                //shuffle
                _availableEventBenefits = _availableEventBenefits.OrderBy(_ => Random.Shared.Next()).ToList();
            }
            return _availableEventBenefits;
        }

        public int GetCurrentParticipationsCount => _eventParticipations.Count;
        public int GetAssignedBenefitsCount => _eventParticipations.Values.Count(v => v.HasValue);
        public bool IsIceBreakerGenerated  => _iceBreakerGenerated;

        public void AddParticipation(int userId, int? benefitId)
        {
            _eventParticipations.Add(userId, benefitId);
            _availableEventBenefits.RemoveAll(eb => eb.Id == benefitId);
        }

        public bool IsOnEvent(int userId)
        {
            return _eventParticipations.ContainsKey(userId);
        }

        public void IceBreakerGenerated()
        {
            _iceBreakerGenerated = true;
        }
    }
}

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
        private List<EventBenefit> _eventBenefits = new();
        private bool _iceBreakerGenerated = false;
        private int _countOfGeneratedBenefits = 0;
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
                if (_todayEvent != null)
                {
                    _iceBreakerGenerated = false;
                    _eventParticipations = _todayEvent.EventParticipations.Select(ep => ep.UserId).ToList();
                    _eventBenefits = await _eventService.GetEventBenefits(cancellationToken);
                    _countOfGeneratedBenefits = _todayEvent.EventParticipations.Count(ep => ep.EventBenefitId.HasValue);
                }
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
                _availableEventBenefits = _countOfGeneratedBenefits > 0
                    ? _eventBenefits.Where(eb => !eb.IsOneTimeBenefit).ToList()
                : _eventBenefits;

                //shuffle
                _availableEventBenefits = _availableEventBenefits.OrderBy(_ => Random.Shared.Next()).ToList();
            }
            return _availableEventBenefits;
        }

        public int GetCurrentParticipationsCount => _eventParticipations.Count;
        public int GetAssignedBenefitsCount => _countOfGeneratedBenefits;
        public bool IsIceBreakerGenerated  => _iceBreakerGenerated;

        public void AddParticipation(int userId, int? benefitId)
        {
            _eventParticipations.Add(userId);
            if (benefitId.HasValue)
            {
                _availableEventBenefits.RemoveAll(eb => eb.Id == benefitId);
                _countOfGeneratedBenefits++;
            }
        }

        public bool IsOnEvent(int userId)
        {
            return _eventParticipations.Contains(userId);
        }

        public void IceBreakerGenerated()
        {
            _iceBreakerGenerated = true;
        }
    }
}

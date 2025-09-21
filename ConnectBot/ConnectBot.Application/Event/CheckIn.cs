using ConnectBot.Application.Cache;
using ConnectBot.Application.Constants;
using ConnectBot.Application.Models;
using ConnectBot.Domain.Entities;
using ConnectBot.Domain.Interfaces;
using MediatR;

namespace ConnectBot.Application.Event
{
    public class CheckIn
    {
        public class Command : MessageCommand
        {

        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ITelegramBotService _botService;
            private readonly IApplicationDbContext _context;
            private readonly UserCache _userCache;
            private readonly EventCache _eventCache;
            private readonly Random _random;
            private const double AssignedTaskUsersPercent = 0.4;

            public Handler(ITelegramBotService botService, IApplicationDbContext context, UserCache userCache, EventCache eventCache)
            {
                _botService = botService;
                _context = context;
                _userCache = userCache;
                _eventCache = eventCache;
                _random = new Random();
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var currentUser = await _userCache.GetUserByChatId(request.Message.Chat.Id, cancellationToken);
                if (currentUser == null)
                {
                    throw new Exception("User not found");
                }

                var todayEvent = await _eventCache.GetTodayEvent(cancellationToken);
                if (todayEvent == null)
                {
                    await _botService.SendMessage(request.Message.Chat.Id, TextConstants.NotFoundTodayEventText);
                    return;
                }

                if (_eventCache.IsOnEvent(currentUser.Id))
                {
                    await _botService.SendMessage(request.Message.Chat.Id, TextConstants.AlreadyCheckedInText);
                    return;
                }
                object locker = new();
                int uniqueNumber, teamColorId;

                EventBenefit? eventBenefit;

                lock (locker)
                {
                    uniqueNumber = _eventCache.GetCurrentParticipationsCount + 1;

                    teamColorId = uniqueNumber % todayEvent.NumberOfTeams;
                    if (teamColorId == 0)
                    {
                        teamColorId = todayEvent.NumberOfTeams;
                    }

                    eventBenefit = GenerateAssignedBenefit(uniqueNumber);
                    _eventCache.AddParticipation(currentUser.Id, eventBenefit?.Id);
                }

                var entity = new EventParticipation
                {
                    CheckedInAt = DateTime.UtcNow,
                    EventId = todayEvent.Id,
                    UserId = currentUser.Id,
                    TeamColorId = teamColorId,
                    UniqueNumber = uniqueNumber,
                    EventBenefitId = eventBenefit?.Id
                };
                await _context.EventParticipations.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var teamColor = await _eventCache.GetTeamColorById(teamColorId, cancellationToken);

                var eventBenefitText = eventBenefit != null
                    ? TextConstants.BenefitText(eventBenefit.Content, eventBenefit.EventBenefitType)
                    : string.Empty;

                await _botService.SendMessage(request.Message.Chat.Id,
                    TextConstants.CheckedInText(entity.UniqueNumber.ToString(),
                        $"{teamColor.Name} {teamColor.ColorSymbol}", todayEvent.Name, eventBenefitText));
            }

            private EventBenefit? GenerateAssignedBenefit(int userUniqueNumber)
            {
                var countOfAssignedTasks = _eventCache.GetAssignedBenefitsCount;
                var probability = AssignedTaskUsersPercent * userUniqueNumber - countOfAssignedTasks;

                probability = Math.Min(1.0, Math.Max(0.0, probability));
                var rnd = _random.NextDouble();

                if (rnd < probability)
                {
                    var benefits = _eventCache.GetAvailableEventBenefits();
                    return benefits.First();
                }

                return null;
            }
        }
    }
}
using ConnectBot.Application.Cache;
using ConnectBot.Application.Constants;
using ConnectBot.Application.Models;
using ConnectBot.Domain.Entities;
using ConnectBot.Domain.Interfaces;
using MediatR;
using System.Text.RegularExpressions;

namespace ConnectBot.Application.Event
{
    public class ManualCheckIn
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

            public Handler(ITelegramBotService botService, IApplicationDbContext context, UserCache userCache, EventCache eventCache)
            {
                _botService = botService;
                _context = context;
                _userCache = userCache;
                _eventCache = eventCache;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var currentUser = await _userCache.GetUserByChatId(request.Message.Chat.Id, cancellationToken);
                if (currentUser == null)
                {
                    throw new Exception("User not found");
                }

                if (!currentUser.IsAdmin)
                {
                    throw new Exception("Manual check in forbidden");
                }

                if (!_userCache.IsUserInMode(currentUser.ChatId, UserState.ManualCheckInMode))
                {
                    await _botService.SendMessage(request.Message.Chat.Id, "User not in manual check in mode");
                }

                var todayEvent = await _eventCache.GetTodayEvent(cancellationToken);
                if (todayEvent == null)
                {
                    await _botService.SendMessage(request.Message.Chat.Id, TextConstants.NotFoundTodayEventText);
                    return;
                }
                string pattern = @"^\s*(\w+)\s+(\w+)\s*$";

                Match match = Regex.Match(request.Message.Text, pattern);
                if (!match.Success)
                {
                    await _botService.SendMessage(request.Message.Chat.Id, TextConstants.WrongCommandOrMessageText);
                    return;
                }
                string firstName = match.Groups[1].Value;
                string lastName = match.Groups[2].Value;

                var passiveUser = new User
                {
                    ChatId = currentUser.ChatId,
                    IsAdmin = false,
                    UserName = "anonymous",
                    FirstName = firstName,
                    LastName = lastName
                };

                await _context.Users.AddAsync(passiveUser, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var uniqueNumber = _eventCache.GetCurrentParticipationsCount + 1;

                var teamColorId = uniqueNumber % todayEvent.NumberOfTeams;
                if (teamColorId == 0)
                {
                    teamColorId = todayEvent.NumberOfTeams;
                }

                var entity = new EventParticipation
                {
                    CheckedInAt = DateTime.UtcNow,
                    EventId = todayEvent.Id,
                    UserId = passiveUser.Id,
                    TeamColorId = teamColorId,
                    UniqueNumber = uniqueNumber
                };
                await _context.EventParticipations.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                _eventCache.AddParticipation(passiveUser.Id);

                var teamColor = await _eventCache.GetTeamColorById(teamColorId, cancellationToken);

                await _botService.SendMessage(request.Message.Chat.Id,
                    TextConstants.CheckedInText(entity.UniqueNumber.ToString(),
                        $"{teamColor.Name} {teamColor.ColorSymbol}", todayEvent.Name));
            }
        }
    }
}

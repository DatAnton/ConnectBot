using ConnectBot.Application.Cache;
using ConnectBot.Application.Constants;
using ConnectBot.Application.Models;
using ConnectBot.Domain.Entities;
using ConnectBot.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            private readonly IUserService _userService;

            public Handler(ITelegramBotService botService, IApplicationDbContext context, UserCache userCache, EventCache eventCache, IUserService userService)
            {
                _botService = botService;
                _context = context;
                _userCache = userCache;
                _eventCache = eventCache;
                _userService = userService;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var currentUser = await _userCache.GetUserByChatId(request.Message.Chat.Id, cancellationToken);
                var testUser =
                    await _context.Users.FirstOrDefaultAsync(u => u.ChatId == request.Message.Chat.Id,
                        cancellationToken);
                var testUser2 = await _userService.GetUserByChatId(request.Message.Chat.Id, cancellationToken);
                await _context.Logs.AddAsync(new Log()
                {
                    ChatId = request.Message.Chat.Id,
                    Message =
                        $"{testUser?.Id} - {testUser?.ChatId}, ${request.Message.Chat.Id == testUser?.ChatId}, ${_userCache._users.Count}, {testUser2?.ChatId}",
                    CreatedAt = DateTime.UtcNow
                });
                await _context.SaveChangesAsync(cancellationToken);
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
                    UserId = currentUser.Id,
                    TeamColorId = teamColorId,
                    UniqueNumber = uniqueNumber
                };
                await _context.EventParticipations.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                _eventCache.AddParticipation(currentUser.Id);

                var teamColor = await _eventCache.GetTeamColorById(teamColorId, cancellationToken);

                await _botService.SendMessage(request.Message.Chat.Id,
                    TextConstants.CheckedInText(entity.UniqueNumber.ToString(),
                        $"{teamColor.Name} {teamColor.ColorSymbol}", todayEvent.Name));
            }
        }
    }
}

using ConnectBot.Application.Cache;
using ConnectBot.Application.Constants;
using ConnectBot.Application.Models;
using ConnectBot.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConnectBot.Application.Event
{
    public class Participatings
    {
        public class Command : MessageCommand
        {

        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ITelegramBotService _botService;
            private readonly IApplicationDbContext _context;
            private readonly EventCache _eventCache;
            private readonly UserCache _userCache;

            public Handler(ITelegramBotService botService, IApplicationDbContext context, EventCache eventCache,
                UserCache userCache)
            {
                _botService = botService;
                _context = context;
                _eventCache = eventCache;
                _userCache = userCache;
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
                    throw new Exception("Forbidden action");
                }

                var todayEvent = await _eventCache.GetTodayEvent(cancellationToken);

                if (todayEvent == null)
                {
                    await _botService.SendMessage(request.Message.Chat.Id, TextConstants.NotFoundTodayEventText);
                    return;
                }

                var users = await _context.EventParticipations.Include(ep => ep.User)
                    .Where(ep => ep.EventId == todayEvent.Id).OrderBy(ep => ep.UniqueNumber)
                    .Select(x => $"{x.UniqueNumber}. {x.User.DisplayName} {x.TeamColor.ColorSymbol}")
                    .ToListAsync(cancellationToken);

                var usersText = string.Join("\r\n", users);

                await _botService.SendMessage(request.Message.Chat.Id, TextConstants.AllParticipationsText(todayEvent.Name, usersText));
            }
        }
    }
}

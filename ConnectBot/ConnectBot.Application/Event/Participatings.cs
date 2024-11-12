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
            private readonly IEventService _eventService;

            public Handler(ITelegramBotService botService, IApplicationDbContext context, IEventService eventService)
            {
                _botService = botService;
                _context = context;
                _eventService = eventService;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var currentUser =
                    await _context.Users.FirstOrDefaultAsync(u => u.ChatId == request.Message.Chat.Id, cancellationToken);
                if (currentUser == null)
                {
                    throw new Exception("User not found");
                }

                if (currentUser.ChatId != UtilConstants.adminChatId)
                {
                    throw new Exception("Forbidden action");
                }

                var todayEvent = await _eventService.GetTodayEvent(cancellationToken);

                if (todayEvent == null)
                {
                    throw new Exception("Not found event");
                }

                var users = await _context.EventParticipations.Include(ep => ep.User).Select(x =>
                    $"{x.UniqueNumber}. {x.User.DisplayName} ({x.TeamColor.ColorSymbol})").ToListAsync(cancellationToken);

                var usersText = string.Join("\r\n", users);

                await _botService.SendMessage(request.Message.Chat.Id, TextConstants.AllParticipationsText(todayEvent.Name, usersText));
            }
        }
    }
}

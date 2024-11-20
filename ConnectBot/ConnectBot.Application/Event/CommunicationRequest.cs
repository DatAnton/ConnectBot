using ConnectBot.Application.Cache;
using ConnectBot.Application.Constants;
using ConnectBot.Application.Models;
using ConnectBot.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConnectBot.Application.Event
{
    public class CommunicationRequest 
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

            public Handler(ITelegramBotService botService, IApplicationDbContext context, UserCache userCache,
                EventCache eventCache)
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
                var todayEvent = await _eventCache.GetTodayEvent(cancellationToken);

                if (todayEvent == null)
                {
                    await _botService.SendMessage(request.Message.Chat.Id, TextConstants.NotFoundTodayEventText);
                    return;
                }

                var requestExists = await _context.CommunicationRequests.AnyAsync(
                    cr => cr.AuthorId == currentUser.Id && cr.EventId == todayEvent.Id, cancellationToken);
                if (requestExists)
                {
                    await _botService.SendMessage(request.Message.Chat.Id, TextConstants.CommunicationRequestAlreadyExistsResponseText);
                    return;
                }

                var entity = new Domain.Entities.CommunicationRequest
                {
                    AuthorId = currentUser.Id,
                    CreatedAt = DateTime.UtcNow,
                    EventId = todayEvent.Id,
                };
                await _context.CommunicationRequests.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                await _botService.SendMessage(request.Message.Chat.Id, TextConstants.CommunicationRequestResponseText);

                foreach (var adminChat in await _userCache.GetAdminChatIds(cancellationToken))
                {
                    await _botService.SendMessage(adminChat, TextConstants.CommunicationRequestHandlerText(currentUser.DisplayName));
                }
            }
        }
    }
}

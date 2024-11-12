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
                var todayEvent = await _eventService.GetTodayEvent(cancellationToken);

                var entity = new Domain.Entities.CommunicationRequest
                {
                    AuthorId = currentUser.Id,
                    CreatedAt = DateTime.UtcNow,
                    EventId = todayEvent.Id,
                };
                await _context.CommunicationRequests.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                await _botService.SendMessage(request.Message.Chat.Id, TextConstants.CommunicationRequestResponseText);

                await _botService.SendMessage(UtilConstants.adminChatId, TextConstants.CommunicationRequestHandlerText(currentUser.DisplayName));
            }
        }
    }
}

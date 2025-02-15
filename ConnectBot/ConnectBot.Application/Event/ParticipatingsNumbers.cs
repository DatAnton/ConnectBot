using ConnectBot.Application.Cache;
using ConnectBot.Application.Constants;
using ConnectBot.Application.Models;
using ConnectBot.Domain.Interfaces;
using MediatR;

namespace ConnectBot.Application.Event
{
    public class ParticipatingsNumber
    {
        public class Command : MessageCommand
        {

        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ITelegramBotService _botService;
            private readonly EventCache _eventCache;
            private readonly UserCache _userCache;

            public Handler(ITelegramBotService botService, EventCache eventCache,
                UserCache userCache)
            {
                _botService = botService;
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

                var usersNumbersText = string.Join("\r\n", Enumerable.Range(1, _eventCache.GetCurrentParticipationsCount).ToList());

                await _botService.SendMessage(request.Message.Chat.Id, usersNumbersText);
            }
        }
    }
}

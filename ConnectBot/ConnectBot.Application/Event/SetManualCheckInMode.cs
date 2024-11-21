using ConnectBot.Application.Cache;
using ConnectBot.Application.Constants;
using ConnectBot.Application.Models;
using ConnectBot.Domain.Interfaces;
using MediatR;

namespace ConnectBot.Application.Event
{
    public class SetManualCheckInMode
    {
        public class Command : MessageCommand
        {

        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ITelegramBotService _botService;
            private readonly UserCache _userCache;
            private readonly EventCache _eventCache;

            public Handler(ITelegramBotService botService, UserCache userCache, EventCache eventCache)
            {
                _botService = botService;
                _userCache = userCache;
                _eventCache = eventCache;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var todayEvent = await _eventCache.GetTodayEvent(cancellationToken);
                if (todayEvent == null)
                {
                    await _botService.SendMessage(request.Message.Chat.Id, TextConstants.NotFoundTodayEventText);
                    return;
                }
                _userCache.SetUserMode(request.Message.Chat.Id, UserState.ManualCheckInMode);
                await _botService.SendMessage(request.Message.Chat.Id, TextConstants.SetManualCheckInModeText);
            }
        }
    }
}
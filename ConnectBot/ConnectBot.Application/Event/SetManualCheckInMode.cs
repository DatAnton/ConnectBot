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

            public Handler(ITelegramBotService botService, UserCache userCache)
            {
                _botService = botService;
                _userCache = userCache;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                _userCache.SetUserMode(request.Message.Chat.Id, UserState.ManualCheckInMode);
                await _botService.SendMessage(request.Message.Chat.Id, TextConstants.SetManualCheckInModeText);
            }
        }
    }
}
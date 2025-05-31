using ConnectBot.Application.Cache;
using ConnectBot.Application.Constants;
using ConnectBot.Application.Models;
using ConnectBot.Domain.Interfaces;
using MediatR;

namespace ConnectBot.Application.Event
{
    public class CommunicationRedirect
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
                await _botService.SendMessage(request.Message.Chat.Id, TextConstants.NoCommunicationPartnerResponse);

                var fromUser = await _userCache.GetUserByChatId(request.Message.Chat.Id, cancellationToken);

                foreach (var adminChat in await _userCache.GetAdmins(cancellationToken))
                {
                    if (_eventCache.IsOnEvent(adminChat.Key))
                    {
                        await _botService.SendMessage(adminChat.Value, TextConstants.NoCommunicationPartnerAdminInfo(fromUser.DisplayName));
                    }
                }
            }
        }
    }
}

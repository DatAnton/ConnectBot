using ConnectBot.Application.Cache;
using ConnectBot.Application.Constants;
using ConnectBot.Application.Models;
using ConnectBot.Domain.Interfaces;
using MediatR;

namespace ConnectBot.Application.Menu
{
    public class AdminPanel
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
                var currentUser = await _userCache.GetUserByChatId(request.Message.Chat.Id, cancellationToken);
                if (currentUser == null)
                {
                    throw new Exception("User not found");
                }

                if (!currentUser.IsAdmin)
                {
                    await _botService.SendMessage(request.Message.Chat.Id, TextConstants.WrongCommandOrMessageText);
                    return;
                }

                _userCache.SetAdminPanel(request.Message.Chat.Id, true);

                await _botService.SendMessage(request.Message.Chat.Id, $"{TextConstants.AdminPanelCommand}:");
            }
        }
    }
}

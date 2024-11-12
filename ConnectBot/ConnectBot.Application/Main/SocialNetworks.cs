using ConnectBot.Application.Constants;
using ConnectBot.Application.Models;
using ConnectBot.Domain.Interfaces;
using MediatR;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConnectBot.Application.Main
{
    public class SocialNetworks 
    {
        public class Command : MessageCommand
        {

        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ITelegramBotService _botService;

            public Handler(ITelegramBotService botService)
            {
                _botService = botService;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var keyBoard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl(TextConstants.SocialNetworksInstagramText, "https://www.instagram.com/_connect4you_/"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl(TextConstants.SocialNetworksTelegramNewsText, "https://t.me/silayouth")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl(TextConstants.SocialNetworksSiteText, "https://www.silachurch.fi/")
                    }
                });

                await _botService.SendMessage(request.Message.Chat.Id, TextConstants.SocialNetworksText, replyMarkup: keyBoard);
            }
        }
    }
}

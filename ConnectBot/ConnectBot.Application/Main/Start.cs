using ConnectBot.Application.Constants;
using ConnectBot.Application.Models;
using ConnectBot.Domain.Interfaces;
using MediatR;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConnectBot.Application.Main
{
    public class Start 
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
                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData(TextConstants.OkText, UtilConstants.AcceptedPrivacyPolicyOk),
                        InlineKeyboardButton.WithCallbackData(TextConstants.NoText, UtilConstants.AcceptedPrivacyPolicyNo)
                    }
                });

                await _botService.SendMessage(request.Message.Chat.Id, TextConstants.PrivacyPolicyText, replyMarkup: inlineKeyboard);
            }
        }
    }

}

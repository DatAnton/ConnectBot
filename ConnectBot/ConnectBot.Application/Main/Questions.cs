using ConnectBot.Application.Constants;
using ConnectBot.Application.Models;
using ConnectBot.Domain.Interfaces;
using MediatR;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConnectBot.Application.Main
{
    public class Questions
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
                        InlineKeyboardButton.WithUrl(TextConstants.AskQuestionHereText, "https://www.survio.com/survey/d/K3N9D2E7N1N8J9H0N"),
                    }
                });

                await _botService.SendMessage(request.Message.Chat.Id, TextConstants.AskQuestionsHeaderText, replyMarkup: keyBoard);
            }
        }
    }
}

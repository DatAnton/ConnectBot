using ConnectBot.Application.Constants;
using ConnectBot.Application.Models;
using ConnectBot.Domain.Interfaces;
using MediatR;

namespace ConnectBot.Application.Main
{
    public class WrongCommand
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
                await _botService.SendMessage(request.Message.Chat.Id, TextConstants.WrongCommandOrMessageText);
            }
        }
    }
}

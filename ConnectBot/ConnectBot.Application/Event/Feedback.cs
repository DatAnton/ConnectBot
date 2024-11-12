using ConnectBot.Application.Cache;
using ConnectBot.Application.Constants;
using ConnectBot.Application.Models;
using ConnectBot.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConnectBot.Application.Event
{
    public class Feedback 
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
                _userCache.SetUserFeedbackMode(request.Message.Chat.Id, UserState.FeedbackMode);
                await _botService.SendMessage(request.Message.Chat.Id, TextConstants.FeedbackText);
            }
        }
    }
}

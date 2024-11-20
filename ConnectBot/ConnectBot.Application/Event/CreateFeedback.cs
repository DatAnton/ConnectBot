using ConnectBot.Application.Cache;
using ConnectBot.Application.Constants;
using ConnectBot.Application.Models;
using ConnectBot.Domain.Interfaces;
using MediatR;

namespace ConnectBot.Application.Event
{
    public class CreateFeedback
    {
        public class Command : MessageCommand
        {

        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ITelegramBotService _botService;
            private readonly IApplicationDbContext _context;
            private readonly UserCache _userCache;

            public Handler(ITelegramBotService botService, IApplicationDbContext context, UserCache userCache)
            {
                _botService = botService;
                _context = context;
                _userCache = userCache;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var currentUser = await _userCache.GetUserByChatId(request.Message.Chat.Id, cancellationToken);
                if (currentUser == null)
                {
                    throw new Exception("User not found");
                }

                if (!_userCache.IsUserInMode(currentUser.ChatId, UserState.FeedbackMode))
                {
                    await _botService.SendMessage(request.Message.Chat.Id, "User not in feedback mode");
                }

                if (string.IsNullOrEmpty(request.Message.Text))
                {
                    await _botService.SendMessage(request.Message.Chat.Id, "Empty user message in feedback");
                }

                var entity = new Domain.Entities.Feedback
                {
                    AuthorId = currentUser.Id,
                    CreatedAt = DateTime.UtcNow,
                    Text = request.Message.Text
                };
                await _context.Feedbacks.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                await _botService.SendMessage(request.Message.Chat.Id, TextConstants.FeedbackResponseText);

                foreach (var adminChat in await _userCache.GetAdminChatIds(cancellationToken))
                {
                    await _botService.SendMessage(adminChat, TextConstants.NewFeedbackHandlerText(currentUser.DisplayName, entity.Text));
                }
            }
        }
    }
}

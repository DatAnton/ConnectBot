using ConnectBot.Application.Cache;
using ConnectBot.Application.Constants;
using ConnectBot.Application.Models;
using ConnectBot.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
                var currentUser =
                    await _context.Users.FirstOrDefaultAsync(u => u.ChatId == request.Message.Chat.Id, cancellationToken);
                if (currentUser == null)
                {
                    throw new Exception("User not found");
                }

                if (!_userCache.IsUserInFeedbackMode(currentUser.ChatId))
                {
                    throw new Exception("User not in feedback mode");
                }

                if (string.IsNullOrEmpty(request.Message.Text))
                {
                    throw new ArgumentNullException(nameof(request.Message.Text), "Empty user message in feedback");
                }

                var entity = new Domain.Entities.Feedback
                {
                    AuthorId = currentUser.Id,
                    CreatedAt = DateTime.UtcNow,
                    Text = request.Message.Text
                };
                await _context.Feedbacks.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                _userCache.SetUserFeedbackMode(currentUser.ChatId, UserState.None);

                await _botService.SendMessage(request.Message.Chat.Id, TextConstants.FeedbackResponseText);

                await _botService.SendMessage(UtilConstants.adminChatId, TextConstants.NewFeedbackHandlerText(currentUser.DisplayName, entity.Text));
            }
        }
    }
}

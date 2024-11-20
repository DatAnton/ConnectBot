using ConnectBot.Application.Cache;
using ConnectBot.Application.Models;
using ConnectBot.Domain.Entities;
using ConnectBot.Domain.Interfaces;
using MediatR;

namespace ConnectBot.Application.Users
{
    public class CreateUser
    {
        public class Command : CallbackQueryCommand
        {

        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IApplicationDbContext _context;
            private readonly UserCache _userCache;

            public Handler(IApplicationDbContext context, UserCache userCache)
            {
                _context = context;
                _userCache = userCache;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var existedUser = await _userCache.GetUserByChatId(request.CallbackQuery.From.Id, cancellationToken);
                if (existedUser == null)
                {
                    var user = new User
                    {
                        ChatId = request.CallbackQuery.From.Id,
                        FirstName = request.CallbackQuery.From.FirstName,
                        LastName = request.CallbackQuery.From.LastName,
                        UserName = request.CallbackQuery.From.Username,
                        IsAdmin = false
                    };
                    await _context.Users.AddAsync(user, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
        }
    }
}

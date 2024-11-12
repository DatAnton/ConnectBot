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

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                // validate that user is not registered yet
                var user = new User
                {
                    ChatId = request.CallbackQuery.From.Id,
                    FirstName = request.CallbackQuery.From.FirstName,
                    LastName = request.CallbackQuery.From.LastName,
                    UserName = request.CallbackQuery.From.Username,
                };
                await _context.Users.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}

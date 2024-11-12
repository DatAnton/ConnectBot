using ConnectBot.Application.Constants;
using ConnectBot.Application.Models;
using ConnectBot.Domain.Entities;
using ConnectBot.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConnectBot.Application.Event
{
    public class CheckIn
    {
        public class Command : MessageCommand
        {

        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ITelegramBotService _botService;
            private readonly IApplicationDbContext _context;
            private readonly IUserService _userService;
            private readonly IEventService _eventService;

            public Handler(ITelegramBotService botService, IApplicationDbContext context, IUserService userService, IEventService eventService)
            {
                _botService = botService;
                _context = context;
                _userService = userService;
                _eventService = eventService;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var currentUser = await _userService.GetUserByChatId(request.Message.Chat.Id, cancellationToken);
                if (currentUser == null)
                {
                    throw new Exception("User not found");
                }

                var todayEvent = await _eventService.GetTodayEvent(cancellationToken);
                if (todayEvent == null)
                {
                    await _botService.SendMessage(request.Message.Chat.Id, TextConstants.NotFoundTodayEventText);
                    return;
                }

                var isAlreadyCheckedIn = await _context.EventParticipations.AnyAsync(
                    ep => ep.EventId == todayEvent.Id && ep.UserId == currentUser.Id, cancellationToken);
                if (isAlreadyCheckedIn)
                {
                    await _botService.SendMessage(request.Message.Chat.Id, TextConstants.AlreadyCheckedInText);
                    return;
                }

                var (uniqueNumber, teamColor) =
                    await _eventService.GetEventUniqueNumberAndTeam(currentUser, todayEvent, cancellationToken);

                var entity = new EventParticipation
                {
                    CheckedInAt = DateTime.UtcNow,
                    EventId = todayEvent.Id,
                    UserId = currentUser.Id,
                    TeamColorId = teamColor.Id,
                    UniqueNumber = uniqueNumber
                };
                await _context.EventParticipations.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);


                await _botService.SendMessage(request.Message.Chat.Id,
                    TextConstants.CheckedInText(entity.UniqueNumber.ToString(),
                        $"{teamColor.Name} {teamColor.ColorSymbol}"));
            }
        }
    }
}

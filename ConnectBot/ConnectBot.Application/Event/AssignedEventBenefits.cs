﻿using ConnectBot.Application.Cache;
using ConnectBot.Application.Constants;
using ConnectBot.Application.Models;
using ConnectBot.Domain.Enums;
using ConnectBot.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConnectBot.Application.Event
{
    public class AssignedEventBenefits
    {
        public class Command : MessageCommand
        {
            
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ITelegramBotService _botService;
            private readonly IApplicationDbContext _context;
            private readonly EventCache _eventCache;
            private readonly UserCache _userCache;

            public Handler(ITelegramBotService botService, IApplicationDbContext context, EventCache eventCache,
                UserCache userCache)
            {
                _botService = botService;
                _context = context;
                _eventCache = eventCache;
                _userCache = userCache;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var currentUser = await _userCache.GetUserByChatId(request.Message.Chat.Id, cancellationToken);
                if (currentUser == null)
                {
                    throw new Exception("User not found");
                }

                if (!currentUser.IsAdmin)
                {
                    throw new Exception("Forbidden action");
                }

                var todayEvent = await _eventCache.GetTodayEvent(cancellationToken);

                if (todayEvent == null)
                {
                    await _botService.SendMessage(request.Message.Chat.Id, TextConstants.NotFoundTodayEventText);
                    return;
                }

                var users = await _context.EventParticipations.Include(ep => ep.User).Include(ep => ep.EventBenefit)
                    .Where(ep => ep.EventId == todayEvent.Id && ep.EventBenefitId.HasValue).OrderBy(ep => ep.UniqueNumber)
                    .ToListAsync(cancellationToken);

                var bonusBenefits = users
                    .Where(e => e.EventBenefit.EventBenefitType == EventBenefitType.Bonus);
                var usersWithBenefits = bonusBenefits.Select(x =>
                    $"{x.UniqueNumber}. {x.User.DisplayName} {x.EventBenefit.Content}");
                var usersWithBenefitsText = string.Join("\r\n", usersWithBenefits);
                await _botService.SendMessage(request.Message.Chat.Id, TextConstants.AllBenefitsText(usersWithBenefitsText, EventBenefitType.Bonus));

                var tasksBenefits = users
                    .Where(e => e.EventBenefit.EventBenefitType == EventBenefitType.Task);
                usersWithBenefits = tasksBenefits.Select(x =>
                    $"{x.UniqueNumber}. {x.User.DisplayName} {x.EventBenefit.Content}");
                usersWithBenefitsText = string.Join("\r\n", usersWithBenefits);
                await _botService.SendMessage(request.Message.Chat.Id, TextConstants.AllBenefitsText(usersWithBenefitsText, EventBenefitType.Task));

            }
        }
    }
}

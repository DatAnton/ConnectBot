using ConnectBot.Application.Cache;
using ConnectBot.Application.Constants;
using ConnectBot.Application.Models;
using ConnectBot.Domain.Entities;
using ConnectBot.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConnectBot.Application.Event
{
    public class IceBreaker
    {
        public class Command : MessageCommand
        {

        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ITelegramBotService _botService;
            private readonly IApplicationDbContext _context;
            private readonly UserCache _userCache;
            private readonly EventCache _eventCache;

            public Handler(ITelegramBotService botService, IApplicationDbContext context, UserCache userCache, EventCache eventCache)
            {
                _botService = botService;
                _context = context;
                _userCache = userCache;
                _eventCache = eventCache;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                await _botService.SendMessage(request.Message.Chat.Id, "Disabled feature");
                return;
                var currentUser = await _userCache.GetUserByChatId(request.Message.Chat.Id, cancellationToken);
                if (currentUser == null)
                {
                    throw new Exception("User not found");
                }

                if (!currentUser.IsAdmin)
                {
                    throw new Exception("Ice breaker is forbidden");
                }

                var todayEvent = await _eventCache.GetTodayEvent(cancellationToken);
                if (todayEvent == null)
                {
                    await _botService.SendMessage(request.Message.Chat.Id, TextConstants.NotFoundTodayEventText);
                    return;
                }

                if (!_eventCache.IsOnEvent(currentUser.Id))
                {
                    await _botService.SendMessage(request.Message.Chat.Id, TextConstants.CheckInRequiredForIceBreakerText);
                    return;
                }

                //if (DateTime.UtcNow < todayEvent.StartDateTime)
                //{
                //    await _botService.SendMessage(request.Message.Chat.Id, TextConstants.WrongCommandOrMessageText);
                //    return;
                //}

                if (_eventCache.IsIceBreakerGenerated && request.Message.Chat.Id != UtilConstants.SuperAdminChatId)
                {
                    await _botService.SendMessage(request.Message.Chat.Id, TextConstants.IceBreakerAlreadyGeneratedText);
                    return;
                }

                //set value to cache
                _eventCache.IceBreakerGenerated();

                List<Tuple<User, User>> iceBreakerList = new();
                var users = await _context.EventParticipations.Include(ep => ep.User)
                    .Where(ep => ep.EventId == todayEvent.Id && !ep.User.IsAdmin)
                    .Select(ep => ep.User)
                    .ToListAsync(cancellationToken);

                if (users.Count % 2 != 0)
                {
                    //add host if needed
                    users.Add(currentUser);
                }

                //shuffle
                users = users.OrderBy(_ => Random.Shared.Next()).ToList();

                //divide on pairs
                for (var i = 0; i < users.Count / 2; i++)
                {
                    var usersPair = new Tuple<User, User>(users[i], users[users.Count - 1 - i]);
                    iceBreakerList.Add(usersPair);
                }

                var userPairsListString = string.Join("\r\n",
                    iceBreakerList.Select(pair => $"{pair.Item1.DisplayName} - {pair.Item2.DisplayName}"));
                
                //send to host
                await _botService.SendMessage(request.Message.Chat.Id, TextConstants.IceBreakerListText(todayEvent.Name, userPairsListString));
                //send to superAdmin
                await _botService.SendMessage(UtilConstants.SuperAdminChatId, TextConstants.IceBreakerListText(todayEvent.Name, userPairsListString));

                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData(TextConstants.NoCommunicationPartner, UtilConstants.CommunicationPartnerNotFound)
                    }
                });

                //send message to users
                foreach (var usersPair in iceBreakerList)
                {
                    await _botService.SendMessage(usersPair.Item1.ChatId, TextConstants.IceBreakerMessageText(usersPair.Item2.DisplayName), replyMarkup: inlineKeyboard);
                    await _botService.SendMessage(usersPair.Item2.ChatId, TextConstants.IceBreakerMessageText(usersPair.Item1.DisplayName), replyMarkup: inlineKeyboard);
                }

                //send success response
                await _botService.SendMessage(request.Message.Chat.Id, TextConstants.IceBreakerGeneratedSuccessfullyText);
            }
        }
    }
}

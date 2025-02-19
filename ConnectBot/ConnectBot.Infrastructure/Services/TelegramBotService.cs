using ConnectBot.Application.Cache;
using ConnectBot.Application.Constants;
using ConnectBot.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConnectBot.Infrastructure.Services
{
    public class TelegramBotService : ITelegramBotService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly UserCache _userCache;
        private readonly IUserService _userService;
        private readonly IApplicationDbContext _applicationDbContext;

        public TelegramBotService(ITelegramBotClient botClient, UserCache userCache, IUserService userService,
            IApplicationDbContext applicationDbContext)
        {
            _botClient = botClient;
            _userCache = userCache;
            _userService = userService;
            _applicationDbContext = applicationDbContext;
        }

        public async Task SendMessage(ChatId chatId, string text, IReplyMarkup? replyMarkup)
        {
            await _botClient.SendTextMessageAsync(chatId, text, replyMarkup: replyMarkup, parseMode: ParseMode.Html);
        }

        public async Task SetUserMenu()
        {
            var commands = new[]
            {
                new BotCommand { Command = CommandConstants.AdminPanelCommand, Description = TextConstants.AdminPanelCommand },
                new BotCommand { Command = CommandConstants.UserPanelCommand, Description = TextConstants.UserPanelCommand }
            };

            //clear for all users
            var users = await _applicationDbContext.Users.Where(u => !u.IsAdmin).ToListAsync(CancellationToken.None);

            foreach (var user in users)
            {
                await _botClient.DeleteMyCommandsAsync(new BotCommandScopeChat
                {
                    ChatId = user.ChatId
                });
                var menuButton = new MenuButtonCommands();
                await _botClient.SetChatMenuButtonAsync(user.ChatId, menuButton);
            }

            //reassign for admins
            var admins = await _userService.GetUserAdmins(CancellationToken.None);
            foreach (var admin in admins)
            {
                await _botClient.SetMyCommandsAsync(commands, new BotCommandScopeChat
                {
                    ChatId = admin.ChatId
                });
                var menuButton = new MenuButtonCommands();
                await _botClient.SetChatMenuButtonAsync(admin.ChatId, menuButton);
            }
        }

        public async Task SetClientLoading(ChatId chatId)
        {
            await _botClient.SendChatActionAsync(chatId, ChatAction.Typing);
        }

        public async Task SendMessage(ChatId chatId, string text)
        {
            var keyboard = _userCache.IsAdminPanel(chatId.Identifier)
                ? GetAdminReplyKeyboardMarkup()
                : GetUserReplyKeyboardMarkup();

            await SendMessage(chatId, text, keyboard);
        }

        private ReplyKeyboardMarkup GetAdminReplyKeyboardMarkup()
        {
            return new ReplyKeyboardMarkup(new[]
            {
                new[]
                {
                    new KeyboardButton(CommandConstants.AllParticipationsCommand)
                },
                new[]
                {
                    new KeyboardButton(CommandConstants.AllParticipationsNumbersCommand),
                },
                new[]
                {
                    new KeyboardButton(CommandConstants.IceBreakerCommand),
                }
            });
        }

        private ReplyKeyboardMarkup GetUserReplyKeyboardMarkup()
        {
            return new ReplyKeyboardMarkup(new[]
            {
                new[]
                {
                    new KeyboardButton(CommandConstants.CheckInCommand),
                    new KeyboardButton(CommandConstants.CommunicationRequestCommand)
                },
                new[]
                {
                    new KeyboardButton(CommandConstants.FeedbackCommand),
                    new KeyboardButton(CommandConstants.SocialNetworksCommand)
                },
                new[]
                {
                    new KeyboardButton(CommandConstants.DonateYouthTeamCommand)
                }
            });
        }
    }
}

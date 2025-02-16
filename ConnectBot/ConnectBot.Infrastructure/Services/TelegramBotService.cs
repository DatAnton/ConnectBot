using ConnectBot.Application.Cache;
using ConnectBot.Application.Constants;
using ConnectBot.Domain.Interfaces;
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

        public TelegramBotService(ITelegramBotClient botClient, UserCache userCache)
        {
            _botClient = botClient;
            _userCache = userCache;
        }

        public async Task SendMessage(ChatId chatId, string text, IReplyMarkup? replyMarkup)
        {
            await _botClient.SendTextMessageAsync(chatId, text, replyMarkup: replyMarkup, parseMode: ParseMode.Html);
        }

        public async Task SetUserMenu(List<long> chatIds)
        {
            var commands = new[]
            {
                new BotCommand { Command = CommandConstants.AdminPanelCommand, Description = TextConstants.AdminPanelCommand },
                new BotCommand { Command = CommandConstants.UserPanelCommand, Description = TextConstants.UserPanelCommand },
            };

            await _botClient.SetMyCommandsAsync(commands);

            var menuButton = new MenuButtonCommands();

            foreach (var chatId in chatIds)
            {
                await _botClient.SetChatMenuButtonAsync(chatId, menuButton);
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

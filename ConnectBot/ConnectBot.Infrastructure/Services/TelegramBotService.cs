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
        private readonly IUserService _userService;

        public TelegramBotService(ITelegramBotClient botClient, UserCache userCache, IUserService userService)
        {
            _botClient = botClient;
            _userCache = userCache;
            _userService = userService;
        }

        public async Task SendMessage(ChatId chatId, string text, IReplyMarkup? replyMarkup)
        {
            //if (fullMessageText.Length > 4096) //telegram limitations
            //{
            //    const int messageCharactersSize = 4000;
            //    var startIndex = 0;
            //    while (startIndex < fullMessageText.Length)
            //    {
            //        var messageText = fullMessageText.Substring(startIndex, messageCharactersSize);
            //        await _botService.SendMessage(request.Message.Chat.Id, messageText);
            //        startIndex += messageCharactersSize;
            //    }
            //}
            //else
            //{
            //    await _botService.SendMessage(request.Message.Chat.Id, fullMessageText);
            //}
            await _botClient.SendTextMessageAsync(chatId, text, replyMarkup: replyMarkup, parseMode: ParseMode.Html);
        }

        public async Task SetUserMenu()
        {
            //clear for all users
            await _botClient.DeleteMyCommandsAsync(new BotCommandScopeDefault());
            await _botClient.SetChatMenuButtonAsync(menuButton: new MenuButtonDefault());

            //reassign for admins
            var menuButton = new MenuButtonCommands();

            var commands = new[]
            {
                new BotCommand { Command = CommandConstants.AdminPanelCommand, Description = TextConstants.AdminPanelCommand },
                new BotCommand { Command = CommandConstants.UserPanelCommand, Description = TextConstants.UserPanelCommand }
            };

            var admins = await _userService.GetUserAdmins(CancellationToken.None);
            foreach (var admin in admins)
            {
                await _botClient.SetMyCommandsAsync(commands, new BotCommandScopeChat
                {
                    ChatId = admin.ChatId
                });
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
                    new KeyboardButton(CommandConstants.AllParticipationsCommand),
                    new KeyboardButton(CommandConstants.BenefitsCommand)
                },
                new[]
                {
                    new KeyboardButton(CommandConstants.AllParticipationsNumbersCommand),
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
                    new KeyboardButton(CommandConstants.QuestionsCommand)
                }
            });
        }
    }
}

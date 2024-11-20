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

        public async Task SendMessage(ChatId chatId, string text)
        {
            await SendMessage(chatId, text, await GetReplyKeyboardMarkup(chatId));
        }

        private async Task<ReplyKeyboardMarkup> GetReplyKeyboardMarkup(ChatId chatId)
        {
            if (await _userCache.IsAdminChat(chatId.Identifier))
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
                        new KeyboardButton(CommandConstants.AllParticipationsCommand),
                        new KeyboardButton(CommandConstants.ManualCheckInCommand)
                    }
                });
            }
            else
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
                    }
                });
            }
        }
    }
}

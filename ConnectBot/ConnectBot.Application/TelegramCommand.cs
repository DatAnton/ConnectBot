using ConnectBot.Domain.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConnectBot.Application
{
    public abstract class TelegramCommand : ITelegramBotCommand
    {
        public abstract string Name { get; }

        public abstract Task Execute(Message message, ITelegramBotClient client);

        public bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text?.ToUpper().Contains(Name) ?? false;
        }

        protected async Task SendMessage(Message message, ITelegramBotClient client, string responseMessage)
        {
            var chatId = message.Chat.Id;
            var keyBoard = new ReplyKeyboardMarkup(
                new[]
                {
                    new KeyboardButton("\U0001F3E0 Main")
                });

            await client.SendTextMessageAsync(chatId, responseMessage, parseMode: ParseMode.Html,
                replyMarkup: keyBoard);
        }
    }
}

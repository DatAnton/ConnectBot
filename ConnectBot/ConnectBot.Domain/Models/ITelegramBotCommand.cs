using Telegram.Bot.Types;
using Telegram.Bot;

namespace ConnectBot.Domain.Models
{
    public interface ITelegramBotCommand
    {
        public Task Execute(Message message, ITelegramBotClient client);
        public bool Contains(Message message);
    }
}

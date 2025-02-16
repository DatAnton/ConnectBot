using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConnectBot.Domain.Interfaces
{
    public interface ITelegramBotService
    {
        Task SendMessage(ChatId chatId, string text, IReplyMarkup? replyMarkup);
        Task SendMessage(ChatId chatId, string text);
        Task SetClientLoading(ChatId chatId);
        Task SetUserMenu(List<long> chatIds);
    }
}

using Telegram.Bot.Types;

namespace ConnectBot.Domain.Interfaces
{
    public interface ICommandUpdateHandler
    {
        Task Handle(Update update);
    }
}

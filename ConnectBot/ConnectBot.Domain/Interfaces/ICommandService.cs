using ConnectBot.Domain.Models;

namespace ConnectBot.Domain.Interfaces
{
    public interface ICommandService
    {
        List<ITelegramBotCommand> Get();
    }
}

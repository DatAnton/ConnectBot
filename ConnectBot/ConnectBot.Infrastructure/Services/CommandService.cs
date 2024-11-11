using ConnectBot.Application.Main;
using ConnectBot.Domain.Interfaces;
using ConnectBot.Domain.Models;

namespace ConnectBot.Infrastructure.Services
{
    public class CommandService : ICommandService
    {
        private readonly List<ITelegramBotCommand> _commands;

        public CommandService()
        {
            _commands = new List<ITelegramBotCommand>
            {
                new StartCommand()
            };
        }
        public List<ITelegramBotCommand> Get()
        {
            return _commands;
        }
    }
}

using ConnectBot.Application.Main;
using ConnectBot.Domain.Interfaces;
using ConnectBot.Domain.Models;

namespace ConnectBot.Infrastructure.Services
{
    public class CommandService : ICommandService
    {
        private readonly List<TelegramCommand> _commands;

        public CommandService()
        {
            _commands = new List<TelegramCommand>
            {
                new StartCommand(),
                new MainCommand()
            };
        }
        public List<TelegramCommand> Get()
        {
            return _commands;
        }
    }
}

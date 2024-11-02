using ConnectBot.Domain.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ConnectBot.Application.Main
{
    public class MainCommand : TelegramCommand
    {
        public override string Name => "MAIN";

        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            await SendMessage(message, client, $"\U0001F3E0 It's main command.");
        }
    }
}

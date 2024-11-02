using ConnectBot.Domain.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ConnectBot.Application.Main
{
    public class StartCommand : TelegramCommand
    {
        public override string Name => "START";
        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            await SendMessage(message, client, $"\U0001F43C Hi {message.From?.FirstName ?? message.From?.Username ?? "Anonymous"}! Nice to see you here!");
        }
    }
}

using Telegram.Bot;
using Telegram.Bot.Types;

namespace ConnectBot.Application.Main
{
    public class StartCommand : TelegramCommand
    {
        public override string Name => "START";
        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            await SendMessage(message, client, TextConstants.PrivacyPolicyText);
            await SendMessage(message, client, TextConstants.WelcomeBotText);
        }
    }
}

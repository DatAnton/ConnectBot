using System.Diagnostics;
using Telegram.Bot;

namespace ConnectBot
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTelegramBotClient(this IServiceCollection serviceCollection,
            BotConfiguration botConfiguration)
        {
            var client = new TelegramBotClient("7571198121:AAEhYDJDesEkGkr8oM27LpUakviYm570RNY");
            var webHook = "https://connect-bot-qa-fee85d1f3d52.herokuapp.com/api/message/update";
            Console.WriteLine(webHook);
            Debug.WriteLine(webHook);
            client.SetWebhookAsync(webHook).Wait();

            return serviceCollection
                .AddTransient<ITelegramBotClient>(_ => client);
        }
    }
}

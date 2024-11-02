using Telegram.Bot;

namespace ConnectBot
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTelegramBotClient(this IServiceCollection serviceCollection,
            BotConfiguration botConfiguration)
        {
            var client = new TelegramBotClient(botConfiguration.BotToken);
            var webHook = $"{botConfiguration.BaseUrl}/api/message/update";
            client.SetWebhookAsync(webHook).Wait();

            return serviceCollection
                .AddTransient<ITelegramBotClient>(_ => client);
        }
    }
}

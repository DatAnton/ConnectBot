using Telegram.Bot;

namespace ConnectBot
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTelegramBotClient(this IServiceCollection serviceCollection)
        {
            var botToken = Environment.GetEnvironmentVariable("BOT_TOKEN");
            var botBaseUrl = Environment.GetEnvironmentVariable("BOT_BASE_URL");
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(botBaseUrl))
            {
                throw new ArgumentException("Bot credentials are empty");
            }
            var client = new TelegramBotClient(botToken);
            var webHook = $"{botBaseUrl}/api/message/update";
            client.SetWebhookAsync(webHook).Wait();

            return serviceCollection
                .AddTransient<ITelegramBotClient>(_ => client);
        }
    }
}

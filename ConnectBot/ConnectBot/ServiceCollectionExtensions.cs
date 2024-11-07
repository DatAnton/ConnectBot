using Telegram.Bot;

namespace ConnectBot
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTelegramBotClient(this IServiceCollection serviceCollection,
            IConfiguration configuration, bool isHerokuServer)
        {
            var botToken = isHerokuServer
                ? Environment.GetEnvironmentVariable("BOT_TOKEN")
                : configuration.GetValue<string>("BOT_TOKEN");

            var botBaseUrl = isHerokuServer
                ? Environment.GetEnvironmentVariable("BOT_BASE_URL")
                : configuration.GetValue<string>("BOT_BASE_URL");

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

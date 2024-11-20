using Telegram.Bot;

namespace ConnectBot.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTelegramBotClient(this IServiceCollection serviceCollection,
            IConfiguration configuration, bool isProduction)
        {
            var botToken = isProduction
                ? Environment.GetEnvironmentVariable("BOT_TOKEN")
                : configuration.GetValue<string>("BOT_TOKEN");

            var botBaseUrl = isProduction
                ? Environment.GetEnvironmentVariable("BOT_BASE_URL")
                : configuration.GetValue<string>("BOT_BASE_URL");

            var secretToken = isProduction
                ? Environment.GetEnvironmentVariable("SECRET_TOKEN")
                : configuration.GetValue<string>("SECRET_TOKEN");

            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(botBaseUrl) || string.IsNullOrEmpty(secretToken))
            {
                throw new ArgumentException("Bot credentials are empty");
            }
            var client = new TelegramBotClient(botToken);
            var webHook = $"{botBaseUrl}/api/message/update";
            client.SetWebhookAsync(webHook, secretToken: secretToken).Wait();

            return serviceCollection
                .AddTransient<ITelegramBotClient>(_ => client);
        }
    }
}

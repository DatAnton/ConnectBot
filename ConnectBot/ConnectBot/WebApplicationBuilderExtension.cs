using Microsoft.EntityFrameworkCore;

namespace ConnectBot
{
    public static class WebApplicationBuilderExtension
    {
        public static IHost MigrateDatabase<T>(this IHost webHost) where T : DbContext
        {
            using var scope = webHost.Services.CreateScope();
            var services = scope.ServiceProvider;
            var db = services.GetRequiredService<T>();
            db.Database.Migrate();

            return webHost;
        }
    }
}

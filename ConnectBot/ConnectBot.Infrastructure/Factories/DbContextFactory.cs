using ConnectBot.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ConnectBot.Infrastructure.Factories
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DbContextFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IApplicationDbContext CreateDbContext()
        {
            var scope = _serviceProvider.CreateScope();
            return scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
        }
    }
}

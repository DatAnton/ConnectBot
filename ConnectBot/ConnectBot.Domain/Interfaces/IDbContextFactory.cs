namespace ConnectBot.Domain.Interfaces
{
    public interface IDbContextFactory
    {
        IApplicationDbContext CreateDbContext();
    }
}

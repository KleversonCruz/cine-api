namespace Infra.Persistence.Initialization
{
    public interface ICustomSeeder
    {
        Task InitializeAsync(CancellationToken cancellationToken);
    }
}
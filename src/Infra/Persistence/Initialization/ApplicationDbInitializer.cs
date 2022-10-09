using Microsoft.EntityFrameworkCore;

namespace Infra.Persistence.Initialization
{
    internal class ApplicationDbInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ApplicationDbSeeder _dbSeeder;

        public ApplicationDbInitializer(ApplicationDbContext dbContext, ApplicationDbSeeder dbSeeder)
        {
            _dbContext = dbContext;
            _dbSeeder = dbSeeder;
        }

        public async Task InitializeAsync(CancellationToken cancellationToken)
        {
            if (_dbContext.Database.GetMigrations().Any())
            {
                if ((await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
                {
                    Console.WriteLine("Applying Migrations");
                    await _dbContext.Database.MigrateAsync(cancellationToken);
                }

                if (await _dbContext.Database.CanConnectAsync(cancellationToken))
                {
                    Console.WriteLine("Connection to Database Succeeded.");
                    await _dbSeeder.SeedDatabaseAsync(_dbContext, cancellationToken);
                }
            }
        }
    }

}

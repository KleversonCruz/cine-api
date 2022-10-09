using Application.Common.Persistence;
using Domain.Common;
using Infra.Common;
using Infra.Persistence.Initialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Persistence
{
    internal static class Startup
    {
        internal static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
        {
            var databaseSettings = config.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
            if (string.IsNullOrEmpty(databaseSettings.ConnectionString))
            {
                throw new InvalidOperationException("DB ConnectionString is not configured.");
            }

            return services
                .Configure<DatabaseSettings>(config.GetSection(nameof(DatabaseSettings)))
                .AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(databaseSettings.ConnectionString))
                .AddTransient<IDatabaseInitializer, DatabaseInitializer>()
                .AddTransient<ApplicationDbInitializer>()
                .AddTransient<ApplicationDbSeeder>()
                .AddTransient<CustomSeederRunner>()

                .AddRepositories();
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(ApplicationDbRepository<>));

            foreach (var aggregateRootType in
                typeof(IAggregateRoot).Assembly.GetExportedTypes()
                    .Where(t => typeof(IAggregateRoot).IsAssignableFrom(t) && t.IsClass)
                    .ToList())
            {
                services.AddScoped(typeof(IReadRepository<>).MakeGenericType(aggregateRootType), sp =>
                    sp.GetRequiredService(typeof(IRepository<>).MakeGenericType(aggregateRootType)));
            }

            return services;
        }
    }
}

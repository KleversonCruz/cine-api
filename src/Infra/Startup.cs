using Infra.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra
{
    public static class Startup
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration config)
        {
            return services
                .AddPersistence(config);
        }
    }
}
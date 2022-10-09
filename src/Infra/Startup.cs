using Infra.Auth;
using Infra.Common;
using Infra.Mailing;
using Infra.Middleware;
using Infra.OpenApi;
using Infra.Persistence;
using Infra.Persistence.Initialization;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infra
{
    public static class Startup
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration config)
        {
            return services
                .AddAuth(config)
                .AddExceptionMiddleware()
                .AddMailing(config)
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddOpenApiDocumentation(config)
                .AddPersistence(config)
                .AddRouting(options => options.LowercaseUrls = true)
                .AddServices();
        }

        public static async Task InitializeDatabasesAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
        {
            using var scope = services.CreateScope();

            await scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>()
                .InitializeDatabasesAsync(cancellationToken);
        }
        public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapControllers().RequireAuthorization();
            return builder;
        }

        public static IApplicationBuilder UseInfra(this IApplicationBuilder builder, IConfiguration config) =>
            builder
                .UseExceptionMiddleware()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseOpenApiDocumentation(config);
    }
}
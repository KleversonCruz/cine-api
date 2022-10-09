using Infra.Auth.Jwt;
using Infra.Auth.Permissions;
using Infra.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Auth
{
    internal static class Startup
    {
        internal static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddPermissions()
                .AddIdentity();
            services.Configure<SecuritySettings>(config.GetSection(nameof(SecuritySettings)));
            return services.AddJwtAuth(config);
        }

        private static IServiceCollection AddPermissions(this IServiceCollection services) =>
        services
            .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
            .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
    }
}

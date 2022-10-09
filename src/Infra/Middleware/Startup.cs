using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Middleware
{
    internal static class Startup
    {
        internal static IServiceCollection AddExceptionMiddleware(this IServiceCollection services) =>
            services.AddScoped<ExceptionMiddleware>();

        internal static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app) =>
            app.UseMiddleware<ExceptionMiddleware>();
    }
}

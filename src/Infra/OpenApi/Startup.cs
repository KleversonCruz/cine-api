using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Infra.OpenApi
{
    internal static class Startup
    {
        internal static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services, IConfiguration config)
        {
            var settings = config.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>();
            if (settings.Enable)
            {
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen(options =>
                {
                    options.EnableAnnotations();
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = settings.Title,
                        Version = settings.Version,
                        Description = settings.Description,
                        Contact = new()
                        {
                            Name = settings.ContactName,
                            Url = new Uri(settings.ContactUrl!)
                        }
                    });

                    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Description = "Input your Bearer token to access this API",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        BearerFormat = "JWT",
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });
                });
                services.AddFluentValidationRulesToSwagger();
            }

            return services;
        }

        internal static IApplicationBuilder UseOpenApiDocumentation(this IApplicationBuilder app, IConfiguration config)
        {
            if (config.GetValue<bool>("SwaggerSettings:Enable"))
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.DocExpansion(DocExpansion.None);
                    options.DefaultModelsExpandDepth(-1);
                });
            }

            return app;
        }
    }
}

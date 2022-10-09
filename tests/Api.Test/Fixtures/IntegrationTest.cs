using Api.Test.Mocks;
using Infra.Persistence;
using Infra.Persistence.Initialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Respawn.Graph;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Api.Test
{
    [Trait("Category", "Integration")]
    public abstract class IntegrationTest : IClassFixture<ApiWebApplicationFactory>, IDisposable
    {
        protected readonly ApiWebApplicationFactory _factory;
        protected HttpClient _client;
        protected DatabaseSettings databaseSettings;

        public IntegrationTest(ApiWebApplicationFactory fixture)
        {
            _factory = fixture;
            _client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddTransient<ICustomSeeder, TestSeeder>();
                });
            }).CreateClientWithTestAuth();

            databaseSettings = _factory.Configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
        }

        public void Dispose()
        {
            ResetDatabaseAsync(databaseSettings.ConnectionString!).Wait();
        }

        private async Task ResetDatabaseAsync(string connection)
        {
            var checkpoint = await Respawner.CreateAsync(connection, new RespawnerOptions
            {
                TablesToIgnore = new Table[]
                {
                    new Table("__EFMigrationsHistory")
                }
            });
            checkpoint.ResetAsync(connection).Wait();
        }
    }
}


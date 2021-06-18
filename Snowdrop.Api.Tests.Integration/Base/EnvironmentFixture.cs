using System;
using System.Threading.Tasks;
using TestEnvironment.Docker;
using TestEnvironment.Docker.Containers.Postgres;
using Xunit;

namespace Snowdrop.Api.Tests.Integration.Base
{
    public sealed class EnvironmentFixture<TStartup> : BaseTest<TStartup>, IAsyncLifetime
    {
        private string _connectionString = default;

        protected override string EnvName => "Test";

        protected override string DatabaseConnectionString => _connectionString;

        protected override string ConfigFileName => "appsettings.Tests.json";

        private const string CONTAINER_NAME = "pgsql-test";
        private DockerEnvironment _environment = default;
        private PostgresContainer _postgresContainer = default;

        public async Task InitializeAsync()
        {
            var builder = new DockerEnvironmentBuilder();
            _environment = builder
                .AddPostgresContainer(CONTAINER_NAME, Guid.NewGuid().ToString())
                .Build();
            await _environment.Up();

            _postgresContainer = _environment.GetContainer<PostgresContainer>(CONTAINER_NAME);

            _connectionString = _postgresContainer.GetConnectionString();
            Initialaze();
        }

        public async Task DisposeAsync()
        { 
            await _environment.DisposeAsync();
            Dispose();
        }
    }
}
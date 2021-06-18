using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Snowdrop.Api.Tests.Integration.Helpers;
using Snowdrop.Auth.Extensions;
using Snowdrop.DAL.Extensions;

namespace Snowdrop.Api.Tests.Integration.Base
{
    public abstract class BaseTest<TStartup> : IDisposable
    {
        protected abstract string DatabaseConnectionString { get; }
        protected abstract string EnvName { get; }
        protected abstract string ConfigFileName { get; }

        public HttpClient HttpClient { get; private set; }
        public IServiceProvider Services { get; private set; }

        private TestServer _server = default;

        protected void Initialaze()
        {
            var assembly = typeof(TStartup).GetTypeInfo().Assembly;
            var relativePath = Path.Combine("");

            var contentRoot = TestProjectHelper.GetProjectPath(relativePath, assembly);
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(contentRoot)
                .AddJsonFile(ConfigFileName);

            var configuration = configurationBuilder.Build();

            var webHostBuilder = new WebHostBuilder()
                .UseContentRoot(contentRoot)
                .ConfigureServices(services => InitializeServices(services, configuration))
                .UseConfiguration(configuration)
                .UseEnvironment(EnvName)
                .UseStartup(typeof(TStartup));

            _server = new TestServer(webHostBuilder);
            HttpClient = _server.CreateClient();
            HttpClient.BaseAddress = new Uri("https://localhost:5001");
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void InitializeServices(IServiceCollection serviceCollection, IConfiguration confgiration)
        {
            var assembly = typeof(TStartup).GetTypeInfo().Assembly;
            var manager = new ApplicationPartManager
            {
                ApplicationParts = { new AssemblyPart(assembly)},
                FeatureProviders = { new ControllerFeatureProvider()}
            };
            serviceCollection.AddSnowdropContext(DatabaseConnectionString);
            serviceCollection.AddSnowdropJwt(confgiration);
            serviceCollection.AddMemorySessionManager();
            serviceCollection.AddSingleton(manager);
            Services = serviceCollection.BuildServiceProvider();
        }

        public void Dispose()
        {
            _server.Dispose();
            HttpClient.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
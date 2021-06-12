using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Snowdrop.DAL.Extensions;

namespace Snowdrop.Db
{
    class Program
    {
        private static Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");
                using var host = CreateHostBuilder(args).Build();

                var cts = new CancellationTokenSource();
                cts.CancelAfter(5000);

                return host.RunAsync(cts.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
                throw;
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, serviceCollection) =>
                {
                    Console.WriteLine("asdfsdf---"+_.Configuration.GetConnectionString("SnowdropContext"));
                    serviceCollection.AddSnowdropContext(_.Configuration.GetConnectionString("SnowdropContext"));
                });
    }
}
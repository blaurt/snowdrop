using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Snowdrop.DAL.Context;

namespace Snowdrop.DAL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSnowdropContext(this IServiceCollection serviceCollection, string connectionString)
        {
            Console.WriteLine("connectionString: "+connectionString);
            serviceCollection.AddDbContext<SnowdropContext>(
                options => options
                    .UseLazyLoadingProxies()
                    .UseNpgsql(connectionString, builder => builder.MigrationsAssembly("Snowdrop.DAL"))
            );
        }
    }
}
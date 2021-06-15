using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Snowdrop.DAL.Context;
using Snowdrop.DAL.Repositories;

namespace Snowdrop.DAL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSnowdropContext(this IServiceCollection serviceCollection, string connectionString)
        {
            Console.WriteLine("connectionString: " + connectionString);
            serviceCollection.AddDbContext<SnowdropContext>(
                options => options
                    .UseLazyLoadingProxies()
                    // TODO replace assembly name
                    .UseNpgsql(connectionString, builder => builder.MigrationsAssembly("Snowdrop.DAL"))
            );

            serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        public static void AddSnowdropContextInMemory(this IServiceCollection serviceCollection, string dbName)
        {
            serviceCollection.AddDbContext<SnowdropContext>(
                options => options.UseInMemoryDatabase(dbName)
            );
            
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
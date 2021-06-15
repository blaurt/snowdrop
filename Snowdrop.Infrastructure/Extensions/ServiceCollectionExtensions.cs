using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Snowdrop.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSnowdropMapper(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(exp =>
                exp.AddMaps(Assembly.GetAssembly(typeof(ServiceCollectionExtensions))));

        }
    }
}
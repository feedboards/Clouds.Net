using Clouds.Net.AWS.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Clouds.Net.AWS.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAWSServices(this IServiceCollection services, Action<IAWSConfigurator> configure)
        {
            var configurator = new AWSConfigurator(services);

            configure(configurator);

            return configurator.Services;
        }
    }
}

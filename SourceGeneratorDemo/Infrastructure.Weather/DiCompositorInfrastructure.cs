using Microsoft.Extensions.DependencyInjection;
using SourceGeneratorDemo.Infrastructure.Weather.Services.Temperature;

namespace SourceGeneratorDemo.Infrastructure.Weather
{
    public static class DiCompositorInfrastructure
    {
        public static IServiceCollection AddInfrastructureWeather(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ITemperatureService, TemperatureService>();

            return serviceCollection;
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using SourceGeneratorDemo.Core.Services.Weather;

namespace SourceGeneratorDemo.Core
{
    public static class DiCompositorCore
    {
        public static IServiceCollection AddCore(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IWeatherService, WeatherService>();

            return serviceCollection;
        }
    }
}
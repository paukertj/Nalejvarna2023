using AutoMapper;
using SourceGeneratorDemo.Core.Services.Weather;

namespace SourceGeneratorDemo.Api.Controllers
{
    public sealed class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<GetWeatherDomainResponse, WeatherForecastDtoResponse>();
            CreateMap<GetWeatherDomainResponse.Temperature, WeatherForecastDtoResponse.Temperature>();
        }
    }
}

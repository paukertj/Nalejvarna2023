using AutoMapper;
using SourceGeneratorDemo.Infrastructure.Weather.Services.Temperature;

namespace SourceGeneratorDemo.Core.Services.Weather
{
    public sealed class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<GetWeatherDomainRequest, GetTemperatureInfrastructureRequest>();

            CreateMap<GetTemperatureInfrastructureResponse, GetWeatherDomainResponse>();
        }
    }
}

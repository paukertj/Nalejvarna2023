using AutoMapper;
using SourceGeneratorDemo.Core.Services.Weather;

namespace SourceGeneratorDemo.Api.Controllers
{
    public partial class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<GetWeatherDomainResponse, WeatherHistoryDtoResponse>();
        }

#if DEBUG_GENERATOR == false

        public Mapping()
        {
            CreateMap<GetWeatherDomainResponse, WeatherHistoryDtoResponse>();
        }

#endif
    }
}

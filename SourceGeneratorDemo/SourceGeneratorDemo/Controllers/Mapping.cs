using AutoMapper;
using SourceGeneratorDemo.Core.Services.Weather;

namespace SourceGeneratorDemo.Api.Controllers
{
    public partial class Mapping : Profile
    {
#if DEBUG_GENERATOR == false

        public Mapping()
        {
            CreateMap<GetWeatherDomainResponse, WeatherHistoryDtoResponse>();
        }

#endif
    }
}

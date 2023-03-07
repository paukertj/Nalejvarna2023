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
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SourceGeneratorDemo.Core.Services.Weather;

namespace SourceGeneratorDemo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherHistoryController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly IMapper _mapper;

        public WeatherHistoryController(IWeatherService weatherService, IMapper mapper)
        {
            _weatherService = weatherService;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetWeatherHistory")]
        public async ValueTask<WeatherHistoryDtoResponse> Get(DateOnly day, CancellationToken cancellationToken)
        {
            var domainRequest = new GetWeatherDomainRequest
            {
                Day = day,
            };

            var domainResponse = await _weatherService.GetWeatherAsync(domainRequest, cancellationToken);

            var dtoResponse = _mapper.Map<WeatherHistoryDtoResponse>(domainResponse);

            return dtoResponse;
        }
    }
}
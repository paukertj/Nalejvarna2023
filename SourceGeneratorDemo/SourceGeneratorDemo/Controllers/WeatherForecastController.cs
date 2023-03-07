using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SourceGeneratorDemo.Core.Services.Weather;
using System.ComponentModel;

namespace SourceGeneratorDemo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly IMapper _mapper;

        public WeatherForecastController(IWeatherService weatherService, IMapper mapper)
        {
            _weatherService = weatherService;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async ValueTask<WeatherForecastDtoResponse> Get(DateOnly from, DateOnly to, CancellationToken cancellationToken)
        {
            var domainRequest = new GetWeatherDomainRequest
            {
                From = from,
                To = to
            };

            var domainResponse = await _weatherService.GetWeatherAsync(domainRequest, cancellationToken);

            var dtoResponse = _mapper.Map<WeatherForecastDtoResponse>(domainResponse);

            return dtoResponse;
        }
    }
}
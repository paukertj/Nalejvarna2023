using AutoMapper;
using SourceGeneratorDemo.Infrastructure.Weather.Services.Temperature;

namespace SourceGeneratorDemo.Core.Services.Weather
{
    internal sealed class WeatherService : IWeatherService
    {
        private readonly ITemperatureService _temperatureService;
        private readonly IMapper _mapper;

        public WeatherService(ITemperatureService temperatureService, IMapper mapper)
        {
            _temperatureService = temperatureService;
            _mapper = mapper;
        }

        public async ValueTask<GetWeatherDomainResponse> GetWeatherAsync(GetWeatherDomainRequest request, CancellationToken cancellationToken)
        {
            // TODO: Validations

            var infrastructureRequest = _mapper.Map<GetTemperatureInfrastructureRequest>(request);

            var infrastructureResponse = await _temperatureService.GetTemperatureAsync(infrastructureRequest, cancellationToken);

            var domainResponse = _mapper.Map<GetWeatherDomainResponse>(infrastructureResponse);

            return domainResponse;
        }
    }
}

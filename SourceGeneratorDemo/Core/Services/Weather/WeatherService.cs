using AutoMapper;
using SourceGeneratorDemo.Core.Services.Validation;
using SourceGeneratorDemo.Infrastructure.Weather.Services.Temperature;

namespace SourceGeneratorDemo.Core.Services.Weather
{
    internal sealed class WeatherService : IWeatherService
    {
        private readonly ITemperatureService _temperatureService;
        private readonly IValidationService _validationService;
        private readonly IMapper _mapper;

        public WeatherService(ITemperatureService temperatureService, IValidationService validationService, IMapper mapper)
        {
            _temperatureService = temperatureService;
            _validationService = validationService;
            _mapper = mapper;
        }

        public async ValueTask<GetWeatherDomainResponse> GetWeatherAsync(GetWeatherDomainRequest request, CancellationToken cancellationToken)
        {
            if (_validationService.CannotBeInFuture(request?.Day) == false)
            {
                return null;
            }

            var infrastructureRequest = _mapper.Map<GetTemperatureInfrastructureRequest>(request);

            var infrastructureResponse = await _temperatureService.GetTemperatureAsync(infrastructureRequest, cancellationToken);

            var domainResponse = _mapper.Map<GetWeatherDomainResponse>(infrastructureResponse);

            return domainResponse;
        }
    }
}

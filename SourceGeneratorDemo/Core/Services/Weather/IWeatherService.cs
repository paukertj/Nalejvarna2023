namespace SourceGeneratorDemo.Core.Services.Weather
{
    public interface IWeatherService
    {
        ValueTask<GetWeatherDomainResponse> GetWeatherAsync(GetWeatherDomainRequest request, CancellationToken cancellationToken);
    }
}

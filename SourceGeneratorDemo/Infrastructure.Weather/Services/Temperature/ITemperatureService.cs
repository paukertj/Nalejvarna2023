namespace SourceGeneratorDemo.Infrastructure.Weather.Services.Temperature
{
    public interface ITemperatureService
    {
        ValueTask<GetTemperatureInfrastructureResponse> GetTemperatureAsync(GetTemperatureInfrastructureRequest request, CancellationToken cancellationToken);
    }
}

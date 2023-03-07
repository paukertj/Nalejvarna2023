namespace SourceGeneratorDemo.Infrastructure.Weather.Services.Temperature
{
    public sealed record GetTemperatureInfrastructureRequest
    {
        public DateOnly Day { get; set; }
    }
}

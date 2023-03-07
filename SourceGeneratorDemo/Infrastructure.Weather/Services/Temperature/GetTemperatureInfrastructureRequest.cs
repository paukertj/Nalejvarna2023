namespace SourceGeneratorDemo.Infrastructure.Weather.Services.Temperature
{
    public sealed record GetTemperatureInfrastructureRequest
    {
        public DateOnly From { get; set; }

        public DateOnly To { get; set; }
    }
}

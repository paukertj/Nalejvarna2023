namespace SourceGeneratorDemo.Infrastructure.Weather.Services.Temperature
{
    public sealed record GetTemperatureInfrastructureResponse
    {
        public DateOnly Day { get; init; }

        public double Temprature { get; init; }

        public string Unit { get; init; }
    }
}

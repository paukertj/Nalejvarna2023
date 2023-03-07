namespace SourceGeneratorDemo.Infrastructure.Weather.Services.Temperature
{
    public sealed record GetTemperatureInfrastructureResponse
    {
        public IReadOnlyList<Temperature> Temperatures { get; init; }

        public sealed record Temperature
        {
            public DateOnly Day { get; init; }

            public double Temprature { get; init; }

            public string Unit { get; init; }
        }
    }
}

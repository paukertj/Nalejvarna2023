namespace SourceGeneratorDemo.Api.Controllers
{
    public sealed record WeatherForecastDtoResponse
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

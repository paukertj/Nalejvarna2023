namespace SourceGeneratorDemo.Core.Services.Weather
{
    public sealed record GetWeatherDomainResponse
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

namespace SourceGeneratorDemo.Core.Services.Weather
{
    public sealed record GetWeatherDomainResponse
    {
        public DateOnly Day { get; init; }

        public double Temprature { get; init; }

        public string Unit { get; init; }
    }
}

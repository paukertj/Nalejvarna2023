namespace SourceGeneratorDemo.Api.Controllers
{
    public sealed record WeatherHistoryDtoResponse
    {
        public DateOnly Day { get; init; }

        public double Temprature { get; init; }

        public string Unit { get; init; }
    }
}

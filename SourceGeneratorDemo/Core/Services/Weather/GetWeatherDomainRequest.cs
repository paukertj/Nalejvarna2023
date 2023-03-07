namespace SourceGeneratorDemo.Core.Services.Weather
{
    public sealed record GetWeatherDomainRequest
    {
        public DateOnly From { get; set; }

        public DateOnly To { get; set; }
    }
}

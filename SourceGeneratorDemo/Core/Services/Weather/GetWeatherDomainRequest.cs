namespace SourceGeneratorDemo.Core.Services.Weather
{
    public sealed record GetWeatherDomainRequest
    {
        public DateOnly Day { get; set; }
    }
}

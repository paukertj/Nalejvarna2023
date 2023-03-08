using System.Collections.Concurrent;

namespace SourceGeneratorDemo.Infrastructure.Weather.Services.Temperature
{
    internal sealed class TemperatureService : ITemperatureService
    {
        private const string Unit = "°C";

        private readonly ConcurrentDictionary<DateOnly, double> _temperatures = new ConcurrentDictionary<DateOnly, double>();
        private readonly Random _random = new Random();
        private readonly object _lock = new object();

        public ValueTask<GetTemperatureInfrastructureResponse> GetTemperatureAsync(GetTemperatureInfrastructureRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            if (TryGetTemperature(request.Day, out double temperature) == false)
            {
                _temperatures[request.Day] = temperature;
            }

            var response = new GetTemperatureInfrastructureResponse
            {
                Day = request.Day,
                Temprature = temperature,
                Unit = Unit
            };

            return ValueTask.FromResult(response);
        }

        private bool TryGetTemperature(DateOnly dateOnly, out double temperature)
        {
            if (_temperatures.TryGetValue(dateOnly, out var t))
            {
                temperature = t;

                return true;
            }

            lock (_lock)
            {
                temperature = _random.Next(-100, 400) / 10d;
            }

            return false;
        }
    }
}

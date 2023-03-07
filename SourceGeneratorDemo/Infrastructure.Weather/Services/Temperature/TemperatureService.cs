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

            var temperatureRecords = new List<GetTemperatureInfrastructureResponse.Temperature>(request.To.DayNumber - request.From.DayNumber);
            
            for (var dateOnly = request.From; dateOnly <= request.To; dateOnly = dateOnly.AddDays(1))
            {
                if (TryGetTemperature(dateOnly, out double temperature) == false)
                {
                    _temperatures[dateOnly] = temperature;
                }

                var temperatureRecord = new GetTemperatureInfrastructureResponse.Temperature
                {
                    Day = dateOnly,
                    Temprature = _temperatures[dateOnly],
                    Unit = Unit
                };

                temperatureRecords.Add(temperatureRecord);
            }

            var response = new GetTemperatureInfrastructureResponse
            {
                Temperatures = temperatureRecords
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

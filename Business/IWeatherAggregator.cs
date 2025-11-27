// Business/IWeatherAggregator.cs
using WeatherApp.Models;

namespace WeatherApp.Business;

public interface IWeatherAggregator
{
    Task<WeatherSummary> BuildSummaryAsync(double lat, double lon, string locationName, CancellationToken ct = default);
}

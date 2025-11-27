// Business/Sources/OpenMeteoForecastSource.cs
using WeatherApp.Services;
using WeatherApp.Models;

namespace WeatherApp.Business.Sources;

public sealed class OpenMeteoForecastSource(IPublicDataService dataService) : IExternalWeatherSource
{
    private readonly IPublicDataService _dataService = dataService;
    public string SourceName => "OpenMeteoForecast";

    public async Task<object?> FetchAsync(double lat, double lon, CancellationToken ct = default)
        => await _dataService.GetForecastAsync(lat, lon, ct);
}

// Business/Sources/OpenMeteoAirQualitySource.cs
using WeatherApp.Services;
using WeatherApp.Models;

namespace WeatherApp.Business.Sources;

public sealed class OpenMeteoAirQualitySource(IPublicDataService dataService) : IExternalWeatherSource
{
    private readonly IPublicDataService _dataService = dataService;
    public string SourceName => "OpenMeteoAirQuality";

    public async Task<object?> FetchAsync(double lat, double lon, CancellationToken ct = default)
        => await _dataService.GetAirQualityAsync(lat, lon, ct);
}

// Services/IPublicDataService.cs
using WeatherApp.Models;

namespace WeatherApp.Services;

public interface IPublicDataService
{
    Task<OpenMeteoForecastDto?> GetForecastAsync(double lat, double lon, CancellationToken ct = default);
    Task<OpenMeteoAirQualityDto?> GetAirQualityAsync(double lat, double lon, CancellationToken ct = default);
}

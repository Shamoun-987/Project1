// Services/IGeocodingService.cs
namespace WeatherApp.Services;

public interface IGeocodingService
{
    Task<(double lat, double lon, string name)?> GetCoordinatesAsync(string city, CancellationToken ct = default);
}

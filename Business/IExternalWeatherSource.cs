// Business/IExternalWeatherSource.cs
namespace WeatherApp.Business;

public interface IExternalWeatherSource
{
    Task<object?> FetchAsync(double lat, double lon, CancellationToken ct = default);
    string SourceName { get; }
}

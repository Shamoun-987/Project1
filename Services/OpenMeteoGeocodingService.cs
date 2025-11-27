// Services/OpenMeteoGeocodingService.cs
using System.Net.Http.Json;

namespace WeatherApp.Services;

public sealed class OpenMeteoGeocodingService(HttpClient http) : IGeocodingService
{
    private readonly HttpClient _http = http;

    public async Task<(double lat, double lon, string name)?> GetCoordinatesAsync(string city, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(city)) return null;

        var url = $"https://geocoding-api.open-meteo.com/v1/search?name={Uri.EscapeDataString(city)}&count=1";
        try
        {
            var result = await _http.GetFromJsonAsync<GeoResponse>(url, cancellationToken: ct);
            var first = result?.Results?.FirstOrDefault();
            if (first is null) return null;
            return (first.Latitude, first.Longitude, string.IsNullOrWhiteSpace(first.Name) ? city : first.Name);
        }
        catch (HttpRequestException) { return null; }
        catch (TaskCanceledException) { return null; }
    }

    private sealed class GeoResponse
    {
        public List<GeoResult>? Results { get; set; }
    }

    private sealed class GeoResult
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; } = "";
    }
}

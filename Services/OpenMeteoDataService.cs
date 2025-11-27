// Services/OpenMeteoDataService.cs
using System.Net.Http.Json;
using WeatherApp.Models;

namespace WeatherApp.Services;

public sealed class OpenMeteoDataService(HttpClient http) : IPublicDataService
{
    private readonly HttpClient _http = http;

    public async Task<OpenMeteoForecastDto?> GetForecastAsync(double lat, double lon, CancellationToken ct = default)
    {
        var url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}" +
                  "&current_weather=true&hourly=temperature_2m,relativehumidity_2m,windspeed_10m&timezone=auto";
        try
        {
            return await _http.GetFromJsonAsync<OpenMeteoForecastDto>(url, cancellationToken: ct);
        }
        catch (HttpRequestException) { return null; }
        catch (TaskCanceledException) { return null; }
    }

    public async Task<OpenMeteoAirQualityDto?> GetAirQualityAsync(double lat, double lon, CancellationToken ct = default)
    {
        var url = $"https://air-quality-api.open-meteo.com/v1/air-quality?latitude={lat}&longitude={lon}" +
                  "&hourly=pm10,pm2_5&timezone=auto";
        try
        {
            return await _http.GetFromJsonAsync<OpenMeteoAirQualityDto>(url, cancellationToken: ct);
        }
        catch (HttpRequestException) { return null; }
        catch (TaskCanceledException) { return null; }
    }
}

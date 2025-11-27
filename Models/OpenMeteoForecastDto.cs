using System.Text.Json.Serialization;

namespace WeatherApp.Models;

public sealed class OpenMeteoForecastDto
{
    // Egenskaper för Current Weather (aktuellt väder)
    [JsonPropertyName("current_weather")]
    public CurrentWeatherDto? CurrentWeather { get; set; }

    // Egenskaper för timdata (Hourly)
    [JsonPropertyName("hourly")]
    public HourlyForecastDto? Hourly { get; set; }
}

public sealed class CurrentWeatherDto
{
    // API-namn: temperature
    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }

    // API-namn: windspeed
    [JsonPropertyName("windspeed")]
    public double Windspeed { get; set; }

    // API-namn: winddirection
    [JsonPropertyName("winddirection")]
    public double WindDirection { get; set; }

    // API-namn: weathercode
    [JsonPropertyName("weathercode")]
    public int Weathercode { get; set; }
}

public sealed class HourlyForecastDto
{
    // Array med timestamp (inte alltid nödvändig att inkludera)
    [JsonPropertyName("time")]
    public string[]? Time { get; set; }

    // API-namn: temperature_2m
    [JsonPropertyName("temperature_2m")]
    public double[]? Temperature_2m { get; set; }

    // API-namn: relativehumidity_2m
    [JsonPropertyName("relativehumidity_2m")]
    public double[]? Relativehumidity_2m { get; set; }

    // API-namn: windspeed_10m
    [JsonPropertyName("windspeed_10m")]
    public double[]? Windspeed_10m { get; set; }
}
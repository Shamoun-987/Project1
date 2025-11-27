// Business/WeatherAggregator.cs
using WeatherApp.Models;

namespace WeatherApp.Business;

public sealed class WeatherAggregator(IEnumerable<IExternalWeatherSource> sources) : IWeatherAggregator
{
    private readonly IEnumerable<IExternalWeatherSource> _sources = sources;

    public async Task<WeatherSummary> BuildSummaryAsync(double lat, double lon, string locationName, CancellationToken ct = default)
    {
        OpenMeteoForecastDto? forecast = null;
        OpenMeteoAirQualityDto? air = null;

        foreach (var src in _sources)
        {
            var result = await src.FetchAsync(lat, lon, ct);
            switch (result)
            {
                case OpenMeteoForecastDto f: forecast = f; break;
                case OpenMeteoAirQualityDto a: air = a; break;
            }
        }

        var currentTemp = forecast?.CurrentWeather?.Temperature ?? double.NaN;
        var currentWind = forecast?.CurrentWeather?.Windspeed ?? double.NaN;
        var weatherCode = forecast?.CurrentWeather?.Weathercode;

        var avg24Temp = AverageFirstN(forecast?.Hourly?.Temperature_2m, 24);
        var avg24Hum = AverageFirstN(forecast?.Hourly?.Relativehumidity_2m, 24);
        var avgPm25 = AverageFirstN(air?.Hourly?.Pm2_5, 24);

        var comfort = CalculateComfortIndex(currentTemp, currentWind);
        var advice = BuildClothingAdvice(currentTemp, currentWind, avgPm25);

        return new WeatherSummary
        {
            LocationName = locationName,
            Latitude = lat,
            Longitude = lon,
            CurrentTemperatureC = currentTemp,
            CurrentWindSpeedMs = currentWind,
            WeatherCode = weatherCode,
            AvgNext24hTempC = avg24Temp,
            AvgNext24hHumidity = avg24Hum,
            AvgPm25 = avgPm25,
            ComfortIndex = comfort,
            ClothingAdvice = advice
        };
    }

    private static double? AverageFirstN(double[]? values, int n)
    {
        if (values == null || values.Length == 0) return null;
        var take = Math.Min(n, values.Length);
        var sum = 0.0;
        for (int i = 0; i < take; i++) sum += values[i];
        return sum / take;
    }

    // Komfortindex: topp runt 23°C; vind minskar komforten
    private static double CalculateComfortIndex(double tempC, double windMs)
    {
        if (double.IsNaN(tempC) || double.IsNaN(windMs)) return 0;
        var tempScore = 100 - Math.Min(100, Math.Abs(tempC - 23) * 5);
        var windPenalty = Math.Min(60, windMs * 8);
        var raw = tempScore - windPenalty;
        return Math.Clamp(raw, 0, 100);
    }

    private static string BuildClothingAdvice(double tempC, double windMs, double? pm25)
    {
        var wind = double.IsNaN(windMs) ? 0 : windMs;
        var temp = double.IsNaN(tempC) ? 10 : tempC;

        var baseAdvice =
            temp < 5 ? "Vinterjacka och mössa." :
            temp < 12 ? "Jacka och långärmat." :
            temp < 18 ? "Lätt jacka eller hoodie." :
            temp < 24 ? "T-shirt, lätt lager." :
            "Tunn T-shirt och vattenflaska.";
            
        if (wind > 6) baseAdvice += " Vind: överväg vindtät ytterjacka.";
        if (pm25 is double p && p > 25) baseAdvice += " Luftkvalitet: undvik hård träning utomhus.";

        return baseAdvice;
    }
}

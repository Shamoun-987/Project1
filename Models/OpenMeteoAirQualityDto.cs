// Models/OpenMeteoAirQualityDto.cs
namespace WeatherApp.Models;

public sealed class OpenMeteoAirQualityDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Timezone { get; set; } = "";
    public HourlyAirDto? Hourly { get; set; }
}

public sealed class HourlyAirDto
{
    public string[] Time { get; set; } = Array.Empty<string>();
    public double[] Pm10 { get; set; } = Array.Empty<double>();
    public double[] Pm2_5 { get; set; } = Array.Empty<double>();
}

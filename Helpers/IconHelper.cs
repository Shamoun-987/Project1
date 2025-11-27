// Helpers/IconHelper.cs
namespace WeatherApp.Helpers;

public static class IconHelper
{
    public static string GetTempIcon(double tempC)
    {
        if (double.IsNaN(tempC)) return "/icons/neutral.png";
        if (tempC < 0) return "/icons/snowflake.png";
        if (tempC < 10) return "/icons/cold.png";
        if (tempC < 20) return "/icons/mild.png";
        if (tempC < 30) return "/icons/sunny.png";
        return "/icons/hot.png";
    }

    public static string GetComfortIcon(double comfortIndex)
    {
        if (comfortIndex >= 80) return "/icons/happy.png";
        if (comfortIndex >= 50) return "/icons/neutral.png";
        return "/icons/sad.png";
    }

    // Enkel mapping av Open-Meteo weathercode → ikon
    public static string GetWeatherCodeIcon(int? code)
    {
        if (code is null) return "/icons/neutral.png";
        return code switch
        {
            0 => "/icons/sunny.png",            // Clear sky
            1 or 2 => "/icons/mild.png",        // Mainly clear/partly cloudy
            3 => "/icons/cold.png",             // Overcast
            45 or 48 => "/icons/cold.png",      // Fog
            51 or 53 or 55 => "/icons/cold.png",// Drizzle
            61 or 63 or 65 => "/icons/sad.png", // Rain
            71 or 73 or 75 => "/icons/snowflake.png", // Snow fall
            80 or 81 or 82 => "/icons/sad.png", // Rain showers
            95 or 96 or 99 => "/icons/sad.png", // Thunderstorm
            _ => "/icons/neutral.png"
        };
    }
}

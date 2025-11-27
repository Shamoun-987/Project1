namespace WeatherApp.Models
{
    public sealed class WeatherSummary
    {
        // 📍 Platsinformation
        public string LocationName { get; set; } = "Lidköping";
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // 🌤 Aktuella värden (nullable för att undvika NaN)
        public double? CurrentTemperatureC { get; set; }
        public double? CurrentWindSpeedMs { get; set; }
        public int? WeatherCode { get; set; }

        // 📊 Bearbetning över 24h
        public double? AvgNext24hTempC { get; set; }
        public double? AvgNext24hHumidity { get; set; }
        public double? AvgPm25 { get; set; }

        // 😊 Komfortindex och klädråd
        public double ComfortIndex { get; set; } // 0–100
        public string ClothingAdvice { get; set; } = "";

        // ✅ Hjälpegenskap för att kolla om data finns
        public bool HasValidCurrentData =>
            CurrentTemperatureC.HasValue &&
            CurrentWindSpeedMs.HasValue &&
            WeatherCode.HasValue;
    }
}

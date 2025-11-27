using WeatherApp.Business;
using WeatherApp.Business.Sources;
using WeatherApp.Services;

var builder = WebApplication.CreateBuilder(args);

// ?? Registrera HttpClient-baserade services
builder.Services.AddHttpClient<IPublicDataService, OpenMeteoDataService>();
builder.Services.AddHttpClient<IGeocodingService, OpenMeteoGeocodingService>();

// ?? Polymorfa datakällor (via interface)
builder.Services.AddScoped<IExternalWeatherSource, OpenMeteoForecastSource>();
builder.Services.AddScoped<IExternalWeatherSource, OpenMeteoAirQualitySource>();

// ?? Affärslogik via interface (VG-krav)
builder.Services.AddScoped<IWeatherAggregator, WeatherAggregator>();

// ?? Razor Pages UI
builder.Services.AddRazorPages();

var app = builder.Build();

// ?? Middleware
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();

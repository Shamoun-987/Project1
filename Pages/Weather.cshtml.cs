// Pages/Weather.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeatherApp.Business;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Pages;

public class WeatherModel(IWeatherAggregator aggregator, IGeocodingService geoService) : PageModel
{
    private readonly IWeatherAggregator _aggregator = aggregator;
    private readonly IGeocodingService _geoService = geoService;

    [BindProperty(SupportsGet = true)]
    public double Lat { get; set; } = 58.5; // Lidköping
    [BindProperty(SupportsGet = true)]
    public double Lon { get; set; } = 13.16;
    [BindProperty(SupportsGet = true)]
    public string LocationName { get; set; } = "Lidköping";
    [BindProperty(SupportsGet = true)]
    public string? City { get; set; }

    public WeatherSummary? Summary { get; private set; }
    public string? Error { get; private set; }

    public async Task OnGetAsync()
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(City))
            {
                var geo = await _geoService.GetCoordinatesAsync(City);
                if (geo is not null)
                {
                    Lat = geo.Value.lat;
                    Lon = geo.Value.lon;
                    LocationName = geo.Value.name;
                }
                else
                {
                    Error = $"Hittade inte platsen '{City}'. Visar standardvärden.";
                }
            }

            Summary = await _aggregator.BuildSummaryAsync(Lat, Lon, LocationName);
            if (Summary is null) Error = "Kunde inte hämta väderdata just nu.";
        }
        catch (Exception)
        {
            Error = "Ett fel inträffade vid hämtning av data.";
        }
    }
}

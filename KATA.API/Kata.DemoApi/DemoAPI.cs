using KATA.Domain.Interfaces.API;
using KATA.Domain.Models;
using Newtonsoft.Json;

namespace Kata.DemoApi;

public class DemoAPI : IDemoAPI
{
    private readonly HttpClient _httpClient;

    public DemoAPI(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("DemoAPIClient");
    }

    public async Task<IEnumerable<WeatherForecast>> GetAlWeatherForecastAsync()
    {
        var response = await _httpClient.GetAsync("weatherforecast");
        response.EnsureSuccessStatusCode();
        var resp = await response.Content.ReadAsStringAsync();
        List<WeatherForecast> weatherForecasts = JsonConvert.DeserializeObject<List<WeatherForecast>>(resp);
        return weatherForecasts;
    }
}

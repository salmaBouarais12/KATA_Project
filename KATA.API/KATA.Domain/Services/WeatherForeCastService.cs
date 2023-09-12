using KATA.Domain.Interfaces.API;
using KATA.Domain.Interfaces.Sevices;
using KATA.Domain.Models;

namespace KATA.Domain.Services;

public class WeatherForeCastService : IWeatherForeCastService
{
    private readonly IDemoAPI _demoAPI;
    public WeatherForeCastService(IDemoAPI demoAPI)
    {
        _demoAPI = demoAPI;
    }

    public async Task<IEnumerable<WeatherForecast>> GetAlWeatherForecastAsync()
    {
        return await _demoAPI.GetAlWeatherForecastAsync();
    }
}

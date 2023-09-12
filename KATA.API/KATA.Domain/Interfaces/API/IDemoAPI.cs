using KATA.Domain.Models;

namespace KATA.Domain.Interfaces.API;

public interface IDemoAPI
{
    Task<IEnumerable<WeatherForecast>> GetAlWeatherForecastAsync();
}

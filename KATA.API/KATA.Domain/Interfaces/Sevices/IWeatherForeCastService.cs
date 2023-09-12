using KATA.Domain.Models;

namespace KATA.Domain.Interfaces.Sevices;

public interface IWeatherForeCastService
{
    Task<IEnumerable<WeatherForecast>> GetAlWeatherForecastAsync();
}

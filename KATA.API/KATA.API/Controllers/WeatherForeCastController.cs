using KATA.API.DTO.Responses;
using KATA.Domain.Interfaces.Sevices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KATA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForeCastController : ControllerBase
    {
        private readonly IWeatherForeCastService _weatherForeCastService;

        public WeatherForeCastController(IWeatherForeCastService weatherForeCastService)
        {
            _weatherForeCastService = weatherForeCastService;
        }

        // GET api/<WeatherForeCastController>/5
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var weatherForeCasts = await _weatherForeCastService.GetAlWeatherForecastAsync();
            var weatherForeCastResult = weatherForeCasts.Select(w => new WeatherForecastResponse(w.Date,w.TemperatureC,w.Summary));
            var weatherForeCastRsponse = new WeatherForecastsResponse(weatherForeCastResult);
            return Ok(weatherForeCastRsponse);
        }
    }
}

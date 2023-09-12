using KATA.Domain.Interfaces.API;
using KATA.Domain.Interfaces.Repositories;
using KATA.Domain.Models;
using KATA.Domain.Services;
using NFluent;
using NSubstitute;

namespace KATA.Test.Domain.Services
{
    public class WeatherForeCastServiceTest
    {
        [Fact]
        public async Task Should_Get_AllWeather()
        {
            //Arrange
            var listOfWeather = new List<WeatherForecast>
            {
                new WeatherForecast {  Date = new DateOnly(2023,05,10), Summary = "2" ,TemperatureC = 3},
           
            };
            IDemoAPI demoAPI = Substitute.For<IDemoAPI>();
            demoAPI.GetAlWeatherForecastAsync().Returns(listOfWeather);
            var weatherForeCastService = new WeatherForeCastService(demoAPI);

            //Act
            var listOfWeathers = await (weatherForeCastService.GetAlWeatherForecastAsync());

            //Assert
            Check.That(listOfWeathers).HasSize(1);
        }
    }
}

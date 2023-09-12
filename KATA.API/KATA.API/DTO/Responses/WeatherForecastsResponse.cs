namespace KATA.API.DTO.Responses;


public record WeatherForecastsResponse(IEnumerable<WeatherForecastResponse> weatherForecastResponse);

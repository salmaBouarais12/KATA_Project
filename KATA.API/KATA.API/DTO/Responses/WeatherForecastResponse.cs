namespace KATA.API.DTO.Responses;

public record WeatherForecastResponse(DateOnly Date, int TemperatureC, string? Summary);

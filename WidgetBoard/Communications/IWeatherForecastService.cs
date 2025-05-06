namespace WidgetBoard.Communications;

public interface IWeatherForecastService
{
    Task<Forecast?> GetForecast(double latitude, double longitude, string apiKey);
}
using WidgetBoard.Communications;

namespace WidgetBoard.ViewModels;

public class WeatherWidgetViewModel : BaseViewModel, IWidgetViewModel
{
    public const string DisplayName = "Weather";
    public int Position { get; set; }
    public string Type => DisplayName;
    private readonly IWeatherForecastService weatherForecastService;
    private readonly ISecureStorage secureStorage;
    private string iconUrl = string.Empty;
    private double temperature;
    private string weather = string.Empty;

    public string IconUrl
    {
        get => iconUrl;
        set => SetProperty(ref iconUrl, value);
    }

    public double Temperature
    {
        get => temperature;
        set => SetProperty(ref temperature, value);
    }

    public string Weather
    {
        get => weather;
        set => SetProperty(ref weather, value);
    }

    public WeatherWidgetViewModel(IWeatherForecastService weatherForecastService, ISecureStorage secureStorage)
    {
        this.weatherForecastService = weatherForecastService;
        this.secureStorage = secureStorage;
        Task.Run(async () => await LoadWeatherForecast());
    }

    private async Task LoadWeatherForecast()
    {
        var apiKey = await this.secureStorage.GetAsync("OpenWeatherApiToken");

        if (apiKey is null)
        {
            return;
        }

        var forecast = await weatherForecastService.
            GetForecast(20.798363, -156.331924, apiKey);

        if (forecast?.Main is null)
        {
            return;
        }

        Temperature = forecast.Main.Temperature;
        Weather = forecast.Weather.First().Main;
        IconUrl = forecast.Weather.First().IconUrl;
    }
}
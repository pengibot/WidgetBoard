using System.Windows.Input;
using WidgetBoard.Communications;
using WidgetBoard.Services;

namespace WidgetBoard.ViewModels;

public class WeatherWidgetViewModel : BaseViewModel, IWidgetViewModel
{
    public const string DisplayName = "Weather";

    private readonly IWeatherForecastService weatherForecastService;
    private readonly ISecureStorage secureStorage;
    private readonly ILocationService locationService;
    private State state;
    private double temperature;
    private string iconUrl = string.Empty;
    private string weather = string.Empty;

    public string Type => DisplayName;

    public int Position { get; set; }

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

    public State State
    {
        get => state;
        set => SetProperty(ref state, value);
    }

    public ICommand LoadWeatherCommand { get; }

    public WeatherWidgetViewModel(
        IWeatherForecastService weatherForecastService,
        ISecureStorage secureStorage,
        ILocationService locationService)
    {
        this.weatherForecastService = weatherForecastService;
        this.locationService = locationService;
        this.secureStorage = secureStorage;

        LoadWeatherCommand = new Command(async () => await LoadWeatherForecast());

        Task.Run(async () => await LoadWeatherForecast());
    }

    private async Task LoadWeatherForecast()
    {
        var apiKey = await this.secureStorage.GetAsync("OpenWeatherApiToken");

        if (apiKey is null)
        {
            return;
        }

        try
        {
            State = State.Loading;

            var location = await this.locationService.GetLocationAsync();
            if (location is null)
            {
                State = State.PermissionError;
                return;
            }

            var forecast = await weatherForecastService.
                GetForecast(location.Latitude, location.Longitude, apiKey);

            if (forecast?.Main is null)
            {
                State = State.Error;
                return;
            }

            Temperature = forecast.Main.Temperature;
            Weather = forecast.Weather.First().Main;
            IconUrl = forecast.Weather.First().IconUrl;

            State = State.Loaded;
        }
        catch (Exception)
        {
            State = State.Error;
        }
    }
}
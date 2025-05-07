using System.Windows.Input;
using WidgetBoard.Data;

namespace WidgetBoard.ViewModels;

public class SettingsPageViewModel : BaseViewModel
{

    private string lastUsedBoard = string.Empty;
    private string openWeatherApiToken = string.Empty;
    private ISecureStorage secureStorage;

    public string LastUsedBoard
    {
        get => lastUsedBoard;
        set => SetProperty(ref lastUsedBoard, value);
    }

    public ICommand ClearLastUsedBoardCommand { get; }

    public string OpenWeatherApiToken
    {
        get => openWeatherApiToken;
        set => SetProperty(ref openWeatherApiToken, value);
    }

    public ICommand SaveApiTokenCommand { get; }

    public SettingsPageViewModel(
        IPreferences preferences,
        IBoardRepository boardRepository,
        ISecureStorage secureStorage)
    {
        this.secureStorage = secureStorage;

        var lastUsedBoardId = preferences.Get("LastUsedBoardId", -1);

        if (lastUsedBoardId != -1)
        {
            LastUsedBoard = boardRepository.LoadBoard(lastUsedBoardId)?.Name ?? string.Empty;
        }

        ClearLastUsedBoardCommand = new Command(() =>
        {
            preferences.Remove("LastUsedBoardId");
            LastUsedBoard = string.Empty;
        });

        SaveApiTokenCommand = new Command(async () =>
        {
            await secureStorage.SetAsync("OpenWeatherApiToken", OpenWeatherApiToken);
        });

        //OpenWeatherApiToken = await secureStorage.GetAsync("OpenWeatherApiToken") ?? string.Empty;
    }

    public async Task InitializeAsync()
    {
        OpenWeatherApiToken = await secureStorage.GetAsync("OpenWeatherApiToken") ?? string.Empty;
    }

}
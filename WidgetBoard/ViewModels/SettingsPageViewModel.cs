using System.Windows.Input;
using WidgetBoard.Data;

namespace WidgetBoard.ViewModels;

public class SettingsPageViewModel : BaseViewModel
{

    private string lastUsedBoard = string.Empty;

    public string LastUsedBoard
    {
        get => lastUsedBoard;
        set => SetProperty(ref lastUsedBoard, value);
    }

    public ICommand ClearLastUsedBoardCommand { get; }

    public SettingsPageViewModel(IPreferences preferences, IBoardRepository boardRepository)
    {
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
    }

}
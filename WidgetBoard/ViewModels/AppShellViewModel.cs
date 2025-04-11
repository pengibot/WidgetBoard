using System.Collections.ObjectModel;
using WidgetBoard.Models;

namespace WidgetBoard.ViewModels;

public class AppShellViewModel : BaseViewModel
{
    public ObservableCollection<Board> Boards { get; } = [];

    public AppShellViewModel()
    {
        Boards.Add(
        new Board
        {
            Name = "My first board",
            NumberOfColumns = 3,
            NumberOfRows = 2
        });
    }

    private Board? currentBoard;
    public Board? CurrentBoard
    {
        get => currentBoard;
        set
        {
            if (SetProperty(ref currentBoard, value) &&
            value is not null)
            {
                BoardSelected(value);
            }
        }
    }

    private async void BoardSelected(Board board)
    {
        await Shell.Current.GoToAsync(
        RouteNames.FixedBoard,
        new Dictionary<string, object>
        {
            { "Board", board }
        });
    }
}
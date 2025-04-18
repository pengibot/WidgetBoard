using System.Collections.ObjectModel;
using WidgetBoard.Data;
using WidgetBoard.Models;

namespace WidgetBoard.ViewModels;

public class AppShellViewModel : BaseViewModel
{
    private readonly IBoardRepository boardRepository;
    private FixedBoard? currentBoard;

    public ObservableCollection<FixedBoard> Boards { get; } = [];

    public FixedBoard? CurrentBoard
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

    public AppShellViewModel(IBoardRepository boardRepository)
    {
        this.boardRepository = boardRepository;

        //Boards.Add(
        //    new FixedBoard
        //    {
        //        Name = "My first board",
        //        NumberOfColumns = 3,
        //        NumberOfRows = 2
        //    });
    }

    public void LoadBoards()
    {
        Boards.Clear();
        var boards = this.boardRepository.ListBoards();
        foreach (var board in boards)
        {
            Boards.Add(board);
        }
    }

    private async void BoardSelected(FixedBoard board)
    {
        await Shell.Current.GoToAsync(
            RouteNames.FixedBoard,
            new Dictionary<string, object>
            {
                { "Board", board }
            });
    }
}
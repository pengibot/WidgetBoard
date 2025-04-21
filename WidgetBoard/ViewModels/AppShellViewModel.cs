using System.Collections.ObjectModel;
using WidgetBoard.Data;
using WidgetBoard.Models;

namespace WidgetBoard.ViewModels;

public class AppShellViewModel : BaseViewModel
{
    private readonly IBoardRepository boardRepository;
    private readonly IPreferences preferences;
    private readonly IDispatcher dispatcher;
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

    public AppShellViewModel(IBoardRepository boardRepository,
        IPreferences preferences,
        IDispatcher dispatcher)
    {
        this.boardRepository = boardRepository;
        this.preferences = preferences;
        this.dispatcher = dispatcher;

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

        var lastUsedBoardId = preferences.Get("LastUsedBoardId", -1);
        FixedBoard? lastUsedBoard = null;

        foreach (var board in boards)
        {
            Boards.Add(board);
            if (lastUsedBoardId == board.Id)
            {
                lastUsedBoard = board;
            }
        }

        if (lastUsedBoard is not null)
        {
            dispatcher.Dispatch(() =>
            {
                BoardSelected(lastUsedBoard);
            });
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
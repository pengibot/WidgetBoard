using System.Collections.ObjectModel;
using System.Windows.Input;
using WidgetBoard.Data;
using WidgetBoard.Models;

namespace WidgetBoard.ViewModels;

public class BoardListPageViewModel : BaseViewModel
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

    public ICommand AddBoardCommand { get; }

    public BoardListPageViewModel(IBoardRepository boardRepository)
    {
        this.boardRepository = boardRepository;
        //Boards.Add(
        //    new FixedBoard
        //    {
        //        Name = "My first board",
        //        NumberOfColumns = 3,
        //        NumberOfRows = 2
        //    });

        AddBoardCommand = new Command(OnAddBoard);
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
                { "Board", board}
            });
    }

    private async void OnAddBoard()
    {
        TaskCompletionSource<FixedBoard?> boardCreated = new();
        await Shell.Current.GoToAsync(
            RouteNames.BoardDetails,
            new Dictionary<string, object>
            {
                { "Created", boardCreated }
            });

        var newBoard = await boardCreated.Task;

        if (newBoard is not null)
        {
            Boards.Add(newBoard);
        }
        await Shell.Current.GoToAsync(RouteNames.BoardDetails);
    }
}
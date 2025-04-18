using SQLite;
using WidgetBoard.Models;

namespace WidgetBoard.Data;

public class SqliteBoardRepository : IBoardRepository
{
    private readonly SQLiteConnection connection;

    public SqliteBoardRepository(IFileSystem fileSystem)
    {
        var dbPath = Path.Combine(fileSystem.AppDataDirectory, "widgetboard_sqlite.db");
        connection = new SQLiteConnection(dbPath);
        connection.CreateTable<FixedBoard>();
        connection.CreateTable<BoardWidget>();
    }

    public void CreateBoard(FixedBoard board)
    {
        connection.Insert(board);
    }

    public void CreateBoardWidget(BoardWidget boardWidget)
    {
        connection.Insert(boardWidget);
    }

    public void DeleteBoard(FixedBoard board)
    {
        connection.Delete(board);
    }

    public IReadOnlyList<FixedBoard> ListBoards()
    {
        return connection.Table<FixedBoard>().OrderBy(b => b.Name).ToList();
    }

    public FixedBoard? LoadBoard(int boardId)
    {
        var board = connection.Find<FixedBoard>(boardId);

        if (board is null)
        {
            return null;
        }

        var widgets = connection.Table<BoardWidget>().Where(w => w.BoardId == boardId).ToList();
        board.BoardWidgets = widgets;

        return board;
    }

    public void UpdateBoard(FixedBoard board)
    {
        connection.Update(board);
    }
}
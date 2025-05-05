using LiteDB;
using WidgetBoard.Models;

namespace WidgetBoard.Data;

public class LiteDBBoardRepository : IBoardRepository
{
    private readonly LiteDatabase database;
    private readonly ILiteCollection<FixedBoard> boardCollection;
    private readonly ILiteCollection<BoardWidget> boardWidgetCollection;

    public LiteDBBoardRepository(IFileSystem fileSystem)
    {
        var dbPath = Path.Combine(fileSystem.AppDataDirectory, "widgetboard_litedb.db");
        var connectionString = new ConnectionString
        {
            Filename = dbPath,
            Connection = ConnectionType.Shared
        };
        database = new LiteDatabase(connectionString);

        boardCollection = database.GetCollection<FixedBoard>("Boards");
        boardWidgetCollection = database.GetCollection<BoardWidget>("BoardWidgets");

        boardCollection.EnsureIndex(b => b.Id, true);
        boardCollection.EnsureIndex(b => b.Name, false);
    }

    public void CreateBoard(FixedBoard board)
    {
        boardCollection.Insert(board);
    }
    public void CreateBoardWidget(BoardWidget boardWidget)
    {
        boardWidgetCollection.Insert(boardWidget);
    }
    public void DeleteBoard(FixedBoard board)
    {
        boardCollection.Delete(board.Id);
    }
    public IReadOnlyList<FixedBoard> ListBoards()
    {
        return boardCollection.Query().OrderBy(b => b.Name).ToList();
    }
    public FixedBoard? LoadBoard(int boardId)
    {
        var board = boardCollection.FindById(boardId);

        if (board is null)
        {
            return null;
        }

        var boardWidgets = boardWidgetCollection.Find(w => w.BoardId == boardId).ToList();
        board.BoardWidgets = boardWidgets;

        return board;
    }
    public void UpdateBoard(FixedBoard board)
    {
        boardCollection.Update(board);
    }
}
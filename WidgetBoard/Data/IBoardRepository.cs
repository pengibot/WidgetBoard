using WidgetBoard.Models;

namespace WidgetBoard.Data;

public interface IBoardRepository
{
    void CreateBoard(FixedBoard board);
    void CreateBoardWidget(BoardWidget boardWidget);
    void DeleteBoard(FixedBoard board);
    IReadOnlyList<FixedBoard> ListBoards();
    FixedBoard? LoadBoard(int boardId);
    void UpdateBoard(FixedBoard board);
}
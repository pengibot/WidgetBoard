namespace WidgetBoard.Models;

public class FixedBoard : Board
{
    public string Name { get; init; } = string.Empty;
    public int NumberOfColumns { get; init; }
    public int NumberOfRows { get; init; }
}
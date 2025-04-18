using System.Collections.ObjectModel;
using WidgetBoard.Models;

namespace WidgetBoard;

public class BoardSearchHandler : SearchHandler
{
    public ObservableCollection<FixedBoard> Boards
    {
        get => (ObservableCollection<FixedBoard>)GetValue(BoardsProperty);
        set => SetValue(BoardsProperty, value);
    }

    public static readonly BindableProperty BoardsProperty = BindableProperty.Create(
        nameof(Boards),
        typeof(ObservableCollection<FixedBoard>),
        typeof(BoardSearchHandler));

    protected override void OnQueryChanged(string oldValue, string newValue)
    {
        base.OnQueryChanged(oldValue, newValue);

        if (string.IsNullOrWhiteSpace(newValue))
        {
            ItemsSource = null;
        }
        else
        {
            ItemsSource = Boards
                .Where(board => board.Name.Contains(newValue, StringComparison.CurrentCultureIgnoreCase))
                .ToList<FixedBoard>();
        }
    }

    protected override async void OnItemSelected(object item)
    {
        base.OnItemSelected(item);

        // Let the animation complete
        await Task.Delay(1000);

        await Shell.Current.GoToAsync(
            RouteNames.FixedBoard,
            new Dictionary<string, object>
            {
                { "Board", (FixedBoard)item}
            });
    }
}
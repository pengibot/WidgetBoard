using WidgetBoard.ViewModels;

namespace WidgetBoard.Views;

public interface IWidgetView
{
    int Position
    {
        get => WidgetViewModel?.Position ?? throw new InvalidOperationException("WidgetViewModel is null.");
        set
        {
            if (WidgetViewModel == null)
                throw new InvalidOperationException("WidgetViewModel is null.");
            WidgetViewModel.Position = value;
        }
    }

    IWidgetViewModel? WidgetViewModel { get; set; }
}

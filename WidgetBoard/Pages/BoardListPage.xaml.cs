using WidgetBoard.ViewModels;

namespace WidgetBoard.Pages;

public partial class BoardListPage : ContentPage
{
    public BoardListPage(BoardListPageViewModel boardListPageViewModel)
    {
        InitializeComponent();
        BindingContext = boardListPageViewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        ((BoardListPageViewModel)BindingContext).LoadBoards();
    }
}
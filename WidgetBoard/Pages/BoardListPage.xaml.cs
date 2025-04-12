using WidgetBoard.ViewModels;

namespace WidgetBoard.Pages;

public partial class BoardListPage : ContentPage
{
    public BoardListPage(BoardListPageViewModel boardListPageViewModel)
    {
        InitializeComponent();
        BindingContext = boardListPageViewModel;
    }
}
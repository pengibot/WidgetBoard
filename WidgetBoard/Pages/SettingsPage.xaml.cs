using WidgetBoard.ViewModels;

namespace WidgetBoard.Pages;

public partial class SettingsPage : ContentPage
{
    private SettingsPageViewModel viewModel;

    public SettingsPage(SettingsPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        this.viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (viewModel != null)
            await viewModel.InitializeAsync();
    }
}
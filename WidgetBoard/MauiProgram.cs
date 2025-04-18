﻿using Microsoft.Extensions.Logging;
using WidgetBoard.Data;
using WidgetBoard.Pages;
using WidgetBoard.ViewModels;
using WidgetBoard.Views;

namespace WidgetBoard;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("VT323-Regular.ttf", "VT323");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddTransient<AppShell>();
        builder.Services.AddTransient<AppShellViewModel>();
        builder.Services.AddSingleton<WidgetFactory>();

        AddPage<BoardDetailsPage, BoardDetailsPageViewModel>(builder.Services, RouteNames.BoardDetails);
        AddPage<BoardListPage, BoardListPageViewModel>(builder.Services, RouteNames.BoardList);
        AddPage<FixedBoardPage, FixedBoardPageViewModel>(builder.Services, RouteNames.FixedBoard);
        AddPage<SettingsPage, SettingsPageViewModel>(builder.Services, RouteNames.Settings);

        WidgetFactory.RegisterWidget<ClockWidgetView, ClockWidgetViewModel>("Clock");
        builder.Services.AddTransient<ClockWidgetView>();
        builder.Services.AddTransient<ClockWidgetViewModel>();
        builder.Services.AddSingleton<WidgetTemplateSelector>();
        builder.Services.AddSingleton(SemanticScreenReader.Default);
        builder.Services.AddSingleton(FileSystem.Current);
        builder.Services.AddTransient<IBoardRepository, SqliteBoardRepository>();

        return builder.Build();
    }

    private static void AddPage<TPage, TViewModel>(
        IServiceCollection services,
        string route)
        where TPage : Page
        where TViewModel : BaseViewModel
    {
        services
            .AddTransient(typeof(TPage))
            .AddTransient(typeof(TViewModel));
        Routing.RegisterRoute(route, typeof(TPage));
    }
}
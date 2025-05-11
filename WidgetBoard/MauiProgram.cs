using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Logging;
using Polly;
using Refit;
using WidgetBoard.Communications;
using WidgetBoard.Data;
using WidgetBoard.Pages;
using WidgetBoard.Services;
using WidgetBoard.ViewModels;
using WidgetBoard.Views;

namespace WidgetBoard;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("SelectAllText", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.SetSelectAllOnFocus(true);
#elif IOS || MACCATALYST
            handler.PlatformView.EditingDidBegin += (s, e) =>
            {
                handler.PlatformView.PerformSelector(new ObjCRuntime.
                Selector("selectAll"), null, 0.0f);
            };
#elif WINDOWS
            handler.PlatformView.GotFocus += (s, e) =>
            {
                handler.PlatformView.SelectAll();
            };
#endif
        });

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
        //builder.Services.AddTransient<IBoardRepository, SqliteBoardRepository>();
        builder.Services.AddTransient<IBoardRepository, LiteDBBoardRepository>();
        builder.Services.AddSingleton(Preferences.Default);
        builder.Services.AddSingleton(SecureStorage.Default);

        builder.Services.AddHttpClient<IWeatherForecastService>()
            .AddStandardResilienceHandler(static options =>
            {
                options.Retry = new HttpRetryStrategyOptions
                {
                    BackoffType = DelayBackoffType.Exponential,
                    MaxRetryAttempts = 3,
                    UseJitter = true,
                    Delay = TimeSpan.FromSeconds(2)
                };
            });
        builder.Services
            .AddRefitClient<IWeatherForecastService>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5"));
        WidgetFactory.RegisterWidget<WeatherWidgetView, WeatherWidgetViewModel>(WeatherWidgetViewModel.DisplayName);
        builder.Services.AddTransient<WeatherWidgetView>();
        builder.Services.AddTransient<WeatherWidgetViewModel>();
        builder.Services.AddSingleton(Geolocation.Default);
        builder.Services.AddSingleton<ILocationService, PlatformLocationService>();

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
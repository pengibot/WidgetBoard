﻿using WidgetBoard.ViewModels;
using WidgetBoard.Views;

namespace WidgetBoard;

public class WidgetFactory
{
    private readonly IServiceProvider serviceProvider;

    public WidgetFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    private static IDictionary<Type, Type> widgetRegistrations = new Dictionary<Type, Type>();

    private static IDictionary<string, Type> widgetNameRegistrations = new Dictionary<string, Type>();

    public static void RegisterWidget<TWidgetView, TWidgetViewModel>(string displayName)
        where TWidgetView : IWidgetView where TWidgetViewModel : IWidgetViewModel
    {
        widgetRegistrations.Add(typeof(TWidgetViewModel), typeof(TWidgetView));
        widgetNameRegistrations.Add(displayName, typeof(TWidgetViewModel));
    }

    public IList<string> AvailableWidgets => widgetNameRegistrations.Keys.ToList();

    public IWidgetView? CreateWidget(IWidgetViewModel widgetViewModel)
    {
        if (widgetRegistrations.TryGetValue(widgetViewModel.GetType(), out var widgetViewType))
        {
            var widgetView = (IWidgetView)serviceProvider.GetRequiredService(widgetViewType!);
            widgetView.WidgetViewModel = widgetViewModel;
            return widgetView;
        }
        return null;
    }

    public IWidgetViewModel? CreateWidgetViewModel(string displayName)
    {
        if (widgetNameRegistrations.TryGetValue(displayName, out var widgetViewModelType))
        {
            return (IWidgetViewModel)serviceProvider.GetRequiredService(widgetViewModelType);
        }
        return null;
    }
}
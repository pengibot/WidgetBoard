namespace WidgetBoard.Triggers;

public class ShowOverlayTriggerAction : TriggerAction<VisualElement>
{
    public bool ShowOverlay { get; set; }

    protected override void Invoke(VisualElement sender)
    {
        sender.IsVisible = ShowOverlay;
    }
}
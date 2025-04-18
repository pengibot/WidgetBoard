namespace WidgetBoard.Animations;

internal class Rubberband
{
    public void Rubberband2(VisualElement view)
    {
        var animation = new Animation();

        animation.Add(0.00, 0.30, new Animation(v => view.ScaleX = v, 1.00, 1.25));
        animation.Add(0.00, 0.30, new Animation(v => view.ScaleY = v, 1.00, 0.75));

        animation.Add(0.30, 0.40, new Animation(v => view.ScaleX = v, 1.25, 0.75));
        animation.Add(0.30, 0.40, new Animation(v => view.ScaleY = v, 0.75, 1.25));

        animation.Add(0.40, 0.50, new Animation(v => view.ScaleX = v, 0.75, 1.15));
        animation.Add(0.40, 0.50, new Animation(v => view.ScaleY = v, 1.25, 0.85));

        animation.Add(0.50, 0.65, new Animation(v => view.ScaleX = v, 1.15, 0.95));
        animation.Add(0.50, 0.65, new Animation(v => view.ScaleY = v, 0.85, 1.05));

        animation.Add(0.65, 0.75, new Animation(v => view.ScaleX = v, 0.95, 1.05));
        animation.Add(0.65, 0.75, new Animation(v => view.ScaleY = v, 1.05, 0.95));

        animation.Add(0.75, 1.00, new Animation(v => view.ScaleX = v, 1.05, 1.00));
        animation.Add(0.75, 1.00, new Animation(v => view.ScaleY = v, 0.95, 1.00));

        animation.Commit(view, "RubberbandAnimation", length: 2000);
    }
}

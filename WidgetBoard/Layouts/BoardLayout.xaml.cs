namespace WidgetBoard.Layouts;

public partial class BoardLayout
{
    public BoardLayout()
    {
        InitializeComponent();
    }

    private ILayoutManager? layoutManager;
    public ILayoutManager? LayoutManager
    {
        get => layoutManager;
        set
        {
            layoutManager = value;
            if (layoutManager is not null)
            {
                layoutManager.Board = this;
            }
        }
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        if (layoutManager is not null)
        {
            layoutManager.BindingContext = this.BindingContext;
        }
    }
}
namespace WidgetBoard
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new StateAwareWindow(new AppShell());
        }

        //protected override Window CreateWindow(IActivationState activationState)
        //{
        //    return new StateAwareWindow(MainPage);
        //}
    }
}
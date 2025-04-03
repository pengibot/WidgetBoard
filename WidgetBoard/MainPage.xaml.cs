namespace WidgetBoard
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

#if WINDOWS
            CounterBtn.Text = $"Windows count: {count}";
#elif ANDROID
            CounterBtn.Text = $"Android count: {count}";
#elif IOS
            CounterBtn.Text = $"iOS count: {count}";
#elif MACCATALYST
            CounterBtn.Text = $"MacCatalyst count: {count}";
#endif

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}

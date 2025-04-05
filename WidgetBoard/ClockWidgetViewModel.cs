using System.ComponentModel;
using WidgetBoard;

public class ClockWidgetViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    private readonly Scheduler scheduler = new();
    private DateTime time;
    public DateTime Time
    {
        get
        {
            return time;
        }
        set
        {
            if (time != value)
            {
                time = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Time)));
            }
        }
    }

    public ClockWidgetViewModel()
    {
        SetTime(DateTime.Now);
    }

    public void SetTime(DateTime dateTime)
    {
        Time = dateTime;
        scheduler.ScheduleAction(
        TimeSpan.FromSeconds(1),
        () => SetTime(DateTime.Now));
    }
}
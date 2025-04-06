namespace WidgetBoard;

public class Scheduler
{
    public void ScheduleAction(TimeSpan timeSpan,
    Action action)
    {
        _ = Task.Run(async () =>
        {
            await Task.Delay(timeSpan);
            action.Invoke();
        });
    }
}
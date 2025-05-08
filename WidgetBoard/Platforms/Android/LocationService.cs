namespace WidgetBoard.Services;

public partial class PlatformLocationService : ILocationService
{
    public Task<Location?> GetLocationAsync()
    {
        return Task.FromResult<Location?>(new Location(48.859288, 2.334644));
    }
}
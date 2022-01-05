public class ViewHandler : Handler
{
    public void Handle()
    {
        Logger.LogInfo($"Handling {TableFqn}");
    }
}
using Microsoft.AspNetCore.SignalR;

namespace DemoApp.LoggerHub;

public class LogMessage(int level, string message)
{
    public int Level { get; set; } = level;
    public string Message { get; set; } = message;
    public DateTime TimStamp { get; set; } = DateTime.UtcNow;
}

public interface ILoggerHubClient
{
    Task OnLog(LogMessage message);
}

public class LoggerHub : Hub<ILoggerHubClient>
{
}
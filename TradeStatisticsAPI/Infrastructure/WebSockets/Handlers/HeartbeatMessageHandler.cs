using TradeStatisticsAPI.Infrastructure.WebSockets.Handlers.Interfaces;

namespace TradeStatisticsAPI.Infrastructure.WebSockets.Handlers;

public class HeartbeatMessageHandler : IListenerMessageHandler
{
    public Task HandleMessage(string message)
    {
        Console.WriteLine("Heartbeat received");
        return Task.CompletedTask;
    }
}
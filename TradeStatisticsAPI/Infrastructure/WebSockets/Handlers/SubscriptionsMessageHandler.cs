using TradeStatisticsAPI.Infrastructure.WebSockets.Handlers.Interfaces;

namespace TradeStatisticsAPI.Infrastructure.WebSockets.Handlers;

public class SubscriptionsMessageHandler : IListenerMessageHandler
{
    public Task HandleMessage(string message)
    {
        Console.WriteLine("Subscriptions received");
        return Task.CompletedTask;
    }
}
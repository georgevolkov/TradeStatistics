namespace TradeStatisticsAPI.Infrastructure.WebSockets.Handlers.Interfaces;

public interface IListenerMessageHandler
{
    Task HandleMessage(string message);
}
namespace TradeStatisticsAPI.Infrastructure.WebSockets.Handlers.Interfaces;

public interface IMessageHandlerFactory
{
    IListenerMessageHandler? Create(string eventType);
}
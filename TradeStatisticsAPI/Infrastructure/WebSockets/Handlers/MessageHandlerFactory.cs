using TradeStatisticsAPI.Infrastructure.WebSockets.Handlers.Interfaces;

namespace TradeStatisticsAPI.Infrastructure.WebSockets.Handlers;

public class MessageHandlerFactory : IMessageHandlerFactory
{
    private readonly IEnumerable<IListenerMessageHandler> _handlers;

    public MessageHandlerFactory(IEnumerable<IListenerMessageHandler> handlers)
    {
        _handlers = handlers;
    }

    public IListenerMessageHandler? Create(string eventType)
    {
	    return eventType switch
	    {
		    "Heartbeat"     => _handlers.FirstOrDefault(x => x is HeartbeatMessageHandler),
		    "Subscriptions" => _handlers.FirstOrDefault(x => x is SubscriptionsMessageHandler),
		    "Trade"         => _handlers.FirstOrDefault(x => x is TradeMessageHandler),
		    _               => throw new ArgumentException($"Unknown event type: {eventType}")
	    };
    }
}
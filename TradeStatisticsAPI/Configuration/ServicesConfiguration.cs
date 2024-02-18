using TradeStatisticsAPI.Application.Services;
using TradeStatisticsAPI.Infrastructure.Api;
using TradeStatisticsAPI.Infrastructure.SignalR;
using TradeStatisticsAPI.Infrastructure.WebSockets.Handlers;
using TradeStatisticsAPI.Infrastructure.WebSockets.Handlers.Interfaces;

namespace TradeStatisticsAPI.Configuration;

public static class ServicesConfiguration
{
	public static void AddServices(this IServiceCollection services)
	{
		services.AddScoped<ITradesReceiver, TradesReceiver>();
		services.AddScoped<TradeListener, TradeListener>();
		services.AddScoped<IMessageHandlerFactory, MessageHandlerFactory>();
		services.AddScoped<IListenerMessageHandler, HeartbeatMessageHandler>();
		services.AddScoped<IListenerMessageHandler, TradeMessageHandler>();
		services.AddScoped<IListenerMessageHandler, SubscriptionsMessageHandler>();

		services.AddScoped<TradeAddEventHandler>();
		services.AddScoped<TradeAverageCountHub>();
		services.AddScoped<TradeAverageSumHub>();

		services.AddScoped<ITradeAddService, TradeStoreService>();
		services.AddScoped<ITradeCounterService, TradeStoreService>();
	}
}

using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TradeStatisticsAPI.Configuration.Models;
using TradeStatisticsAPI.Infrastructure.WebSockets.Handlers.Interfaces;
using WebSocketSharp;

namespace TradeStatisticsAPI.Infrastructure.SignalR;

public class TradeListener
{
	private readonly IMessageHandlerFactory     _messageHandlerFactory;
	private readonly TradeListenerConfiguration _configuration;

	public TradeListener(
		IMessageHandlerFactory               messageHandlerFactory,
		IOptions<TradeListenerConfiguration> configuration)
	{
		_messageHandlerFactory = messageHandlerFactory;
		_configuration         = configuration.Value;
	}

	public async Task StartListeningAsync()
	{
		using var ws = new WebSocket(_configuration.UriOfTradeListener);

		ws.OnMessage += async (sender, e) =>
		{
			var eventType = GetEventType(e.Data);
			var handler = _messageHandlerFactory.Create(eventType);
			if (handler == null)
			{
				Console.WriteLine($"No handler for message: {e.Data}");
				return;
			}

			await handler.HandleMessage(e.Data);
		};

		ws.Connect();

		await Task.Delay(Timeout.Infinite);
	}

	public string? GetEventType(string message)
	{
		var messageObject = JsonConvert.DeserializeAnonymousType(message, new { Event = "" });
		return messageObject?.Event;
	}
}
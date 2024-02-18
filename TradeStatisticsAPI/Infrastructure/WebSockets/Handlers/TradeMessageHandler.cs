using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using TradeStatisticsAPI.Application.Models;
using TradeStatisticsAPI.Application.Services;
using TradeStatisticsAPI.Infrastructure.WebSockets.Handlers.Interfaces;
using TradeStatisticsAPI.Infrastructure.WebSockets.Handlers.Models;

namespace TradeStatisticsAPI.Infrastructure.WebSockets.Handlers;

public class TradeMessageHandler : IListenerMessageHandler
{
	private readonly ITradeAddService _tradeAddService;

	public TradeMessageHandler(ITradeAddService tradeAddService)
	{
		_tradeAddService = tradeAddService;
	}

	public Task HandleMessage(string message)
    {
		Console.WriteLine("Trade message received");
		var settings = new JsonSerializerSettings
		{
			FloatFormatHandling = FloatFormatHandling.DefaultValue,
			FloatParseHandling  = FloatParseHandling.Double,
		};

		var model = JsonConvert.DeserializeObject<TradeEventModel>(message, settings);
        if (model == null)
        {
			Console.WriteLine("Error deserializing TradeEventModel");
			return Task.CompletedTask;
		}

        var trade = new TradeShortData(
	        model.Data.TradeGuid,
	        model.Data.TradeDate.AddHours(-5),
	        new decimal(model.Data.Volume));

		_tradeAddService.AddTrade(trade);

		return Task.CompletedTask;
    }
}
using Microsoft.AspNetCore.SignalR;
using TradeStatisticsAPI.Application.Services;

namespace TradeStatisticsAPI.Infrastructure.SignalR;

public class TradeAverageCountHub : Hub
{
	private readonly ITradeAddService     _tradeAddService;
	private readonly ITradeCounterService _tradeCounterService;
	private readonly TradeListener        _tradeListener;

	public TradeAverageCountHub(
		ITradeAddService     tradeAddService,
		ITradeCounterService tradeCounterService,
		TradeListener        tradeListener)
	{
		_tradeAddService     = tradeAddService;
		_tradeCounterService = tradeCounterService;
		_tradeListener       = tradeListener;
	}

	public override async Task OnConnectedAsync()
	{
		await _tradeAddService.InitializeFromApi ();
		var averageCount = await _tradeCounterService.GetAverageCountAsync();
		Console.WriteLine ($"Connected {averageCount}");
		await Clients.All.SendAsync ("ReceiveTradeAverageCount", averageCount);
		await _tradeListener.StartListeningAsync();
	}
}
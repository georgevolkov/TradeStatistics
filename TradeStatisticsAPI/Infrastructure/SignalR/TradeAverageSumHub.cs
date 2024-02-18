using Microsoft.AspNetCore.SignalR;
using TradeStatisticsAPI.Application.Services;

namespace TradeStatisticsAPI.Infrastructure.SignalR;

public class TradeAverageSumHub : Hub
{
	private readonly ITradeCounterService _tradeCounterService;

	public TradeAverageSumHub(ITradeCounterService tradeCounterService)
	{
		_tradeCounterService = tradeCounterService;
	}

	public override async Task OnConnectedAsync()
	{
		var averageSum = await _tradeCounterService.GetAverageSumAsync();
		await Clients.All.SendAsync("ReceiveTradeAverageSum", averageSum);
	}
}

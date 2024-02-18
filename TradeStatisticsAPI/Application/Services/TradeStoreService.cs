using MediatR;
using Microsoft.Extensions.Caching.Memory;
using TradeStatisticsAPI.Application.Models;
using TradeStatisticsAPI.Infrastructure.Api;

namespace TradeStatisticsAPI.Application.Services;

public interface ITradeAddService
{
	Task InitializeFromApi();
    void AddTrade(TradeShortData trade);
}

public interface ITradeCounterService
{
    Task<decimal> GetAverageCountAsync();
    Task<decimal> GetAverageSumAsync();
}

public class TradeStoreService : ITradeAddService, ITradeCounterService
{
	private readonly ITradesReceiver _tradesReceiver;
	private readonly IMemoryCache    _memoryCache;
	private readonly IMediator       _mediator;

	public TradeStoreService(
		ITradesReceiver tradesReceiver,
		IMemoryCache    memoryCache,
		IMediator       mediator)
	{
		_tradesReceiver = tradesReceiver;
		_memoryCache    = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
		_mediator       = mediator ?? throw new ArgumentNullException(nameof(mediator));
	}

	public async Task InitializeFromApi()
	{
		var trades = _memoryCache.GetOrCreate("Trades", entry => new List<TradeShortData>());
		if(trades != null && trades.Any())
			return;

		var apiTrades = await _tradesReceiver.GetTrades();
		foreach (var trade in apiTrades.Trades)
		{
			AddTrade(new TradeShortData(trade.TradeGuid, trade.TradeTimestampUtc.AddHours(6), new decimal(trade.PrimaryCurrencyAmount)));
		}
	}

	public void AddTrade(TradeShortData trade)
	{
		if (trade == null)
			throw new ArgumentNullException(nameof(trade));

		var trades = _memoryCache.GetOrCreate("Trades", entry => new List<TradeShortData>());
		if (trades == null)
			throw new ArgumentNullException(nameof(trades));

		trades.Add(trade);
		_memoryCache.Set("Trades", trades);

		_mediator.Publish(new TradeAddEvent());
	}

	public async Task<decimal> GetAverageCountAsync()
	{
		var trades = _memoryCache.Get<List<TradeShortData>>("Trades");
		if (trades == null || trades.Count == 0)
			return await Task.FromResult(0);
		var count = trades.Count;
		var minutesCount = (decimal)(trades.Max(x => x.TradeDate) - trades.Min(x => x.TradeDate)).TotalMinutes;

		return minutesCount == 0 ? 0 : count / minutesCount;
	}

	public async Task<decimal> GetAverageSumAsync()
	{
		var trades = _memoryCache.Get<List<TradeShortData>>("Trades");
		if (trades == null || trades.Count == 0)
			return await Task.FromResult(0);

		var totalVolume = trades.Sum(x => x.Volume);
		var minutesCount = (trades.Max(x => x.TradeDate) - trades.Min(x => x.TradeDate)).TotalMinutes;

		return minutesCount == 0 ? 0 : totalVolume / (decimal)minutesCount;
	}
}
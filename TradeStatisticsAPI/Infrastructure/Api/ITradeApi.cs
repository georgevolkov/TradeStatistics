using Refit;
using TradeStatisticsAPI.Infrastructure.Models;

namespace TradeStatisticsAPI.Infrastructure.Api;

public interface ITradeApi
{
    [Get("/Public/GetRecentTrades")]
    Task<TradeData> GetRecentTrades(
        [Query] string primaryCurrencyCode,
        [Query] string secondaryCurrencyCode,
        [Query] int numberOfRecentTradesToRetrieve);
}

using Microsoft.Extensions.Options;
using Refit;
using TradeStatisticsAPI.Configuration.Models;
using TradeStatisticsAPI.Infrastructure.Models;

namespace TradeStatisticsAPI.Infrastructure.Api;

public interface ITradesReceiver
{
    public Task<TradeData> GetTrades();
}


public class TradesReceiver : ITradesReceiver
{
    private readonly TradesReceiverConfiguration _config;

    public TradesReceiver(IOptions<TradesReceiverConfiguration> config)
    {
        _config = config.Value;
    }

    public async Task<TradeData> GetTrades()
    {
        var httpClient = new HttpClient { BaseAddress = new Uri(_config.UriOfTrades) };
        var tradeApi = RestService.For<ITradeApi>(httpClient);

        var tradeData =
            await tradeApi.GetRecentTrades(
                _config.PrimaryCurrencyCode,
                _config.SecondaryCurrencyCode,
                _config.NumberOfRecentTradesToRetrieve);

        return tradeData;
    }
}
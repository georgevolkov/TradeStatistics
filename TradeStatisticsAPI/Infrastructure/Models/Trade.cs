namespace TradeStatisticsAPI.Infrastructure.Models;

public record Trade(
    DateTime TradeTimestampUtc,
    double PrimaryCurrencyAmount,
    double SecondaryCurrencyTradePrice,
    string TradeGuid,
    string Taker);

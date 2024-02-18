namespace TradeStatisticsAPI.Infrastructure.Models;

public record TradeData(
    List<Trade> Trades,
    string PrimaryCurrencyCode,
    string SecondaryCurrencyCode,
    DateTime CreatedTimestampUtc
);

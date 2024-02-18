namespace TradeStatisticsAPI.Configuration.Models;

public class TradesReceiverConfiguration
{
	public string UriOfTrades                    { get; set; } = string.Empty;
	public string PrimaryCurrencyCode            { get; set; } = string.Empty;
	public string SecondaryCurrencyCode          { get; set; } = string.Empty;
	public int    NumberOfRecentTradesToRetrieve { get; set; }
}
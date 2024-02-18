namespace TradeStatisticsAPI.Application.Models;

public class TradeShortData
{
	public TradeShortData(
		string         tradeGuid,
		DateTimeOffset tradeDate,
		decimal        volume)
	{
		TradeGuid = tradeGuid;
		TradeDate = tradeDate;
		Volume    = volume;
	}

	public string         TradeGuid { get; private set; }
	public DateTimeOffset TradeDate { get; private set; }
	public decimal        Volume    { get; private set; }
}
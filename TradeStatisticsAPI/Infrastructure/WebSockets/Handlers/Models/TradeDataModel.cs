using TradeStatisticsAPI.Infrastructure.WebSockets.Listener.Models;

namespace TradeStatisticsAPI.Infrastructure.WebSockets.Handlers.Models;

public class TradeDataModel
{
	public string     TradeGuid { get; set; } = String.Empty;
	public DateTime   TradeDate { get; set; }
	public double     Volume    { get; set; }
	public string     BidGuid   { get; set; } = String.Empty;
	public string     OfferGuid { get; set; } = String.Empty;
	public string     Side      { get; set; } = String.Empty;
	public PriceModel Price     { get; set; } = new PriceModel();

	public override string ToString()
	{
		return
			$"TradeGuid: {TradeGuid}, TradeDate: {TradeDate}, Volume: {Volume}, BidGuid: {BidGuid}, OfferGuid: {OfferGuid}, Side: {Side}, Price: {Price}";
	}
}
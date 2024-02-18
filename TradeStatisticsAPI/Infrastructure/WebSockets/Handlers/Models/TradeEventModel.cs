namespace TradeStatisticsAPI.Infrastructure.WebSockets.Handlers.Models;

public class TradeEventModel
{
	public string         Channel { get; set; }
	public int            Nonce   { get; set; }
	public TradeDataModel Data    { get; set; }
	public long           Time    { get; set; }
	public string         Event   { get; set; }

	public override string ToString()
	{
		return $"Channel: {Channel}, Nonce: {Nonce}, TradeData: {Data}, Time: {Time}, Event: {Event}";
	}
}
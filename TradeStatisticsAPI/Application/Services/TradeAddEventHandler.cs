using MediatR;
using Microsoft.AspNetCore.SignalR;
using TradeStatisticsAPI.Infrastructure.SignalR;

namespace TradeStatisticsAPI.Application.Services;

public class TradeAddEvent : INotification
{
}

public class TradeAddEventHandler : INotificationHandler<TradeAddEvent>
{
    private readonly ITradeCounterService _tradeCounterService;
    private readonly IHubContext<TradeAverageCountHub> _tradeAverageCountHub;
    private readonly IHubContext<TradeAverageSumHub> _tradeAverageSumHub;

    public TradeAddEventHandler(
        ITradeCounterService tradeCounterService,
        IHubContext<TradeAverageCountHub> tradeAverageCountHub,
        IHubContext<TradeAverageSumHub> tradeAverageSumHub)
    {
        _tradeCounterService = tradeCounterService;
        _tradeAverageCountHub = tradeAverageCountHub;
        _tradeAverageSumHub = tradeAverageSumHub;
    }

    public async Task Handle(TradeAddEvent notification, CancellationToken cancellationToken)
    {
        var averageCount = await _tradeCounterService.GetAverageCountAsync();
        var averageSum = await _tradeCounterService.GetAverageSumAsync();

        await _tradeAverageCountHub.Clients.All.SendAsync("ReceiveTradeAverageCount", averageCount,
            cancellationToken: cancellationToken);
        await _tradeAverageSumHub.Clients.All.SendAsync("ReceiveTradeAverageSum", averageSum,
            cancellationToken: cancellationToken);
    }
}
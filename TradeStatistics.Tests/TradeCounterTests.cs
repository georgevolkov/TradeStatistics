using AutoFixture;
using Microsoft.Extensions.Caching.Memory;
using TradeStatisticsAPI.Application.Models;
using TradeStatisticsAPI.Application.Services;
using Xunit;

namespace TradeStatistics.Tests;

public class TradeCounterTests : TestBase
{
	[Theory]
	[InlineData(15, 15)]
	[InlineData(10, 15)]
	[InlineData(15, 3)]
	[InlineData(3, 15)]
	public void AverageCountTest(int records, int minutes)
	{
		// Arrange
		var memoryCache = new MemoryCache(new MemoryCacheOptions());
		var tradeCounter = new TradeStoreService(memoryCache, MediatorMock.Object);

		var dateNow = DateTime.Now;
		var startTrade =
			new TradeShortData(Guid.NewGuid().ToString(), dateNow.AddMinutes(-minutes), Fixture.Create<int>());

		var endTrade = new TradeShortData(
			Guid.NewGuid().ToString(),
			dateNow,
			Fixture.Create<int>());

		// Act
		tradeCounter.AddTrade(startTrade);
		for (int i = 1; i < records; i++)
		{
			tradeCounter.AddTrade(endTrade);
		}

		// Assert
		var result = tradeCounter.GetAverageCountAsync().Result;
		var expected =  (decimal)records / minutes;
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData(15, 0.2, 15)]
	[InlineData(10, 2.2, 10)]
	[InlineData(15, 0.3, 3)]
	[InlineData(3, 0.55, 5)]
	public void AverageSumTest(int records, decimal forSum, int minutes)
	{
		// Arrange
		var memoryCache = new MemoryCache(new MemoryCacheOptions());
		var tradeCounter = new TradeStoreService(memoryCache, MediatorMock.Object);

		var dateNow = DateTime.Now;
		var startTrade =
			new TradeShortData(Guid.NewGuid().ToString(), dateNow.AddMinutes(-minutes), forSum);

		var endTrade = new TradeShortData(
			Guid.NewGuid().ToString(),
			dateNow,
			forSum);

		// Act
		tradeCounter.AddTrade(startTrade);
		for (int i = 1; i < records; i++)
		{
			tradeCounter.AddTrade(endTrade);
		}

		// Assert
		var result = tradeCounter.GetAverageSumAsync().Result;
		var expected = (decimal)(records * forSum) / minutes;
		Assert.Equal(expected, result);
	}
}
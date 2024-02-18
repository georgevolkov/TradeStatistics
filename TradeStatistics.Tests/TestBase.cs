using AutoFixture;
using AutoFixture.AutoMoq;
using MediatR;
using Moq;

namespace TradeStatistics.Tests;

public class TestBase
{
	protected IFixture        Fixture      => GetAutoFixture();
	protected Mock<IMediator> MediatorMock { get; private set; }

	public TestBase()
	{
		MediatorMock = new Mock<IMediator>();
	}

	private static IFixture GetAutoFixture()
	{
		var fixture = new Fixture().Customize(new AutoMoqCustomization());
		fixture.Behaviors.Add(new OmitOnRecursionBehavior());
		fixture.Customize<DateOnly>(composer => composer.FromFactory<DateTime>(DateOnly.FromDateTime));

		return fixture;
	}
}
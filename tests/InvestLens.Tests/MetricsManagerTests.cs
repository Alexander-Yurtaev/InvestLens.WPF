using FluentAssertions;
using InvestLens.App.Services;
using InvestLens.Shared.DataAccess.Repositories;
using InvestLens.Shared.Model.Entities;
using InvestLens.Shared.Model.Enums;
using InvestLens.ViewModel.Events;
using InvestLens.ViewModel.Services;
using Moq;
using Xunit.Sdk;

namespace InvestLens.Tests;

public class MetricsManagerTests
{
    [Fact]
    public void GetCurrentPortfolioCost_ShouldReturnPortfolioCost_WhenPortfoliosLoaded()
    {
        var eventAggregatorMock = new Mock<IEventAggregator>();
        eventAggregatorMock
            .Setup(ea => ea.GetEvent<PortfoliosLoadedEvent>())
            .Returns(new PortfoliosLoadedEvent());
        eventAggregatorMock
            .Setup(ea => ea.GetEvent<MetricsManagerInitEvent>())
            .Returns(new MetricsManagerInitEvent());

        var portfoliosManagerMock = new Mock<IPortfoliosManager>();
        portfoliosManagerMock
            .Setup(pm => pm.GetAllPortfolioIds(It.IsAny<PortfolioType>()))
            .Returns([1]);

        var windowManagerMock = new Mock<IWindowManager>();
        
        var repositoryMock = new Mock<ITransactionRepository>();
        repositoryMock
            .Setup(r => r.GetPortfolioCurrentCost(It.IsAny<int>(), It.IsAny<List<Security>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(100);

        var securityRepositoryMock = new Mock<ISecurityRepository>();
        securityRepositoryMock
            .Setup(sr => sr.GetLoadedSecurityListAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var manager = new MetricsManager(
            eventAggregatorMock.Object,
            portfoliosManagerMock.Object,
            windowManagerMock.Object,
            repositoryMock.Object,
            securityRepositoryMock.Object);

        eventAggregatorMock.Object.GetEvent<PortfoliosLoadedEvent>().Publish();

        var cost = manager.GetCurrentPortfolioCost([1]);
        cost.Should().Be(100);
    }
}
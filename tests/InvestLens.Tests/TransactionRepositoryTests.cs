using FluentAssertions;
using InvestLens.DataAccess;
using InvestLens.DataAccess.Repositories;
using InvestLens.DataAccess.Services;
using InvestLens.Model.Entities;
using InvestLens.Model.Entities.Moex;
using InvestLens.Model.Enums;
using InvestLens.Model.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace InvestLens.Tests;

public class TransactionRepositoryTests : IDisposable
{
    private readonly IDatabaseService _databaseService;
    private readonly ITransactionRepository _repository;
    private readonly Mock<IAuthManager> _authManagerMock;
    private readonly Mock<ISecurityRepository> _securityRepositoryMock;

    public TransactionRepositoryTests()
    {
        // Используем InMemory database вместо моков
        var options = new DbContextOptionsBuilder<InvestLensDataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new InvestLensDataContext(options);
        _databaseService = new DatabaseService(context);

        _databaseService.DataContext.Database.EnsureCreated();

        _authManagerMock = new Mock<IAuthManager>();
        _securityRepositoryMock = new Mock<ISecurityRepository>();
        _repository = new TransactionRepository(_databaseService, _authManagerMock.Object);
    }

    public void Dispose()
    {
        _databaseService.DataContext.Database.EnsureDeleted();
        //_databaseService.Dispose();
    }

    [Fact]
    public async Task GetPortfolioCurrentCost_EmptyPortfolio_ReturnsZero()
    {
        // Arrange
        var portfolioId = 1;
        var securities = await _securityRepositoryMock.Object.GetLoadedSecurityListAsync(CancellationToken.None);

        // Act
        var result = await _repository.GetPortfolioCurrentCost(portfolioId, securities, CancellationToken.None);

        // Assert
        result.Should().Be(0);
    }

    [Fact]
    public async Task GetPortfolioCurrentCost_OnlyBuyTransactions_ReturnsCorrectTotal()
    {
        // Arrange
        var portfolioId = 1;
        
        var transactions = new List<Transaction>
        {
            new Transaction
            {
                PortfolioId = portfolioId,
                Symbol = "AAPL",
                Date = DateTime.Now.AddDays(-2),
                Event = TransactionEvent.Buy,
                Quantity = 10,
                Price = 150,
                Id = 1
            },
            new Transaction
            {
                PortfolioId = portfolioId,
                Symbol = "AAPL",
                Date = DateTime.Now.AddDays(-1),
                Event = TransactionEvent.Buy,
                Quantity = 5,
                Price = 155,
                Id = 2
            }
        };

        var history = new List<History>
        {
            new History
            {
                SecId = "AAPL",
                Date = DateTime.Now,
                Close = 160
            },
            new History
            {
                SecId = "AAPL",
                Date = DateTime.Now.AddDays(-1),
                Close = 170
            }
        };

        await _databaseService.DataContext.Transactions.AddRangeAsync(transactions, CancellationToken.None);
        await _databaseService.DataContext.History.AddRangeAsync(history, CancellationToken.None);
        await _databaseService.DataContext.SaveChangesAsync(CancellationToken.None);

        var securities = transactions.Select(t => new Security { SecId = t.Symbol }).ToList();

        // Act
        var result = await _repository.GetPortfolioCurrentCost(portfolioId, securities, CancellationToken.None);

        // Assert
        // Total quantity = 15, Current price = 160, Expected total = 2400
        result.Should().Be(2400);
    }

    [Fact]
    public async Task GetPortfolioCurrentCost_BuyAndSellTransactions_ReturnsCorrectTotal()
    {
        // Arrange
        var portfolioId = 1;
        var transactions = new List<Transaction>
        {
            new Transaction
            {
                PortfolioId = portfolioId,
                Symbol = "AAPL",
                Date = DateTime.Now.AddDays(-3),
                Event = TransactionEvent.Buy,
                Quantity = 20,
                Price = 150,
                Id = 1
            },
            new Transaction
            {
                PortfolioId = portfolioId,
                Symbol = "AAPL",
                Date = DateTime.Now.AddDays(-2),
                Event = TransactionEvent.Sell,
                Quantity = 5,
                Price = 160,
                Id = 2
            },
            new Transaction
            {
                PortfolioId = portfolioId,
                Symbol = "AAPL",
                Date = DateTime.Now.AddDays(-1),
                Event = TransactionEvent.Buy,
                Quantity = 3,
                Price = 155,
                Id = 3
            }
        };

        var history = new List<History>
        {
            new History
            {
                SecId = "AAPL",
                Date = DateTime.Now,
                Close = 170
            },
            new History
            {
                SecId = "AAPL",
                Date = DateTime.Now.AddDays(-1),
                Close = 200
            }
        };

        await _databaseService.DataContext.Transactions.AddRangeAsync(transactions, CancellationToken.None);
        await _databaseService.DataContext.History.AddRangeAsync(history, CancellationToken.None);
        await _databaseService.DataContext.SaveChangesAsync(CancellationToken.None);

        var securities = transactions.Select(t => new Security { SecId = t.Symbol }).ToList();

        // Act
        var result = await _repository.GetPortfolioCurrentCost(portfolioId, securities, CancellationToken.None);

        // Assert
        // Net quantity = 20 - 5 + 3 = 18, Current price = 170, Expected total = 3060
        result.Should().Be(3060);
    }

    [Fact]
    public async Task GetPortfolioCurrentCost_WithSplitTransaction_ReturnsCorrectTotal()
    {
        // Arrange
        var portfolioId = 1;
        var transactions = new List<Transaction>
        {
            new Transaction
            {
                PortfolioId = portfolioId,
                Symbol = "AAPL",
                Date = DateTime.Now.AddDays(-2),
                Event = TransactionEvent.Buy,
                Quantity = 10,
                Price = 150,
                Id = 1
            },
            new Transaction
            {
                PortfolioId = portfolioId,
                Symbol = "AAPL",
                Date = DateTime.Now.AddDays(-1),
                Event = TransactionEvent.Split,
                Quantity = 0,
                Price = 2,  // 2:1 split multiplier
                Id = 2
            }
        };

        var history = new List<History>
        {
            new History
            {
                SecId = "AAPL",
                Date = DateTime.Now,
                Close = 80
            },
            new History
            {
                SecId = "AAPL",
                Date = DateTime.Now.AddDays(-1),
                Close = 90
            }
        };

        await _databaseService.DataContext.Transactions.AddRangeAsync(transactions, CancellationToken.None);
        await _databaseService.DataContext.History.AddRangeAsync(history, CancellationToken.None);
        await _databaseService.DataContext.SaveChangesAsync(CancellationToken.None);

        var securities = transactions.Select(t => new Security { SecId = t.Symbol }).ToList();

        // Act
        var result = await _repository.GetPortfolioCurrentCost(portfolioId, securities, CancellationToken.None);

        // Assert
        // After split: quantity = 10 * 2 = 20, Current price = 80, Expected total = 1600
        result.Should().Be(1600);
    }

    [Fact]
    public async Task GetPortfolioCurrentCost_MultipleSymbols_ReturnsSumOfAll()
    {
        // Arrange
        var portfolioId = 1;
        var transactions = new List<Transaction>
        {
            new Transaction
            {
                PortfolioId = portfolioId,
                Symbol = "AAPL",
                Date = DateTime.Now.AddDays(-1),
                Event = TransactionEvent.Buy,
                Quantity = 10,
                Price = 150,
                Id = 1
            },
            new Transaction
            {
                PortfolioId = portfolioId,
                Symbol = "GOOGL",
                Date = DateTime.Now.AddDays(-1),
                Event = TransactionEvent.Buy,
                Quantity = 5,
                Price = 2800,
                Id = 2
            }
        };

        var history = new List<History>
        {
            new History
            {
                SecId = "AAPL",
                Date = DateTime.Now,
                Close = 160
            },
            new History
            {
                SecId = "AAPL",
                Date = DateTime.Now.AddDays(-1),
                Close = 140
            },

            new History
            {
                SecId = "GOOGL",
                Date = DateTime.Now,
                Close = 2900
            },
            new History
            {
                SecId = "AAPL",
                Date = DateTime.Now.AddDays(-1),
                Close = 3000
            }
        };

        await _databaseService.DataContext.Transactions.AddRangeAsync(transactions, CancellationToken.None);
        await _databaseService.DataContext.History.AddRangeAsync(history, CancellationToken.None);
        await _databaseService.DataContext.SaveChangesAsync(CancellationToken.None);

        var securities = transactions.Select(t => new Security { SecId = t.Symbol }).ToList();

        // Act
        var result = await _repository.GetPortfolioCurrentCost(portfolioId, securities, CancellationToken.None);

        // Assert
        // AAPL: 10 * 160 = 1600
        // GOOGL: 5 * 2900 = 14500
        // Total = 16100
        result.Should().Be(16100);
    }

    [Fact]
    public async Task GetPortfolioCurrentCost_NoPriceInHistory_ReturnsZeroForSymbol()
    {
        // Arrange
        var portfolioId = 1;
        var transactions = new List<Transaction>
        {
            new Transaction
            {
                PortfolioId = portfolioId,
                Symbol = "AAPL",
                Date = DateTime.Now.AddDays(-1),
                Event = TransactionEvent.Buy,
                Quantity = 10,
                Price = 150,
                Id = 1
            }
        };

        // Не добавляем цену в историю
        await _databaseService.DataContext.Transactions.AddRangeAsync(transactions, CancellationToken.None);
        await _databaseService.DataContext.SaveChangesAsync(CancellationToken.None);

        var securities = transactions.Select(t => new Security { SecId = t.Symbol }).ToList();

        // Act
        var result = await _repository.GetPortfolioCurrentCost(portfolioId, securities, CancellationToken.None);

        // Assert
        result.Should().Be(0);
    }

    [Fact]
    public async Task GetPortfolioCurrentCost_DifferentPortfolios_DoesNotMixTransactions()
    {
        // Arrange
        var portfolioId1 = 1;
        var portfolioId2 = 2;

        var transactions = new List<Transaction>
        {
            new Transaction
            {
                PortfolioId = portfolioId1,
                Symbol = "AAPL",
                Date = DateTime.Now.AddDays(-1),
                Event = TransactionEvent.Buy,
                Quantity = 10,
                Price = 150,
                Id = 1
            },
            new Transaction
            {
                PortfolioId = portfolioId2,
                Symbol = "AAPL",
                Date = DateTime.Now.AddDays(-1),
                Event = TransactionEvent.Buy,
                Quantity = 5,
                Price = 150,
                Id = 2
            }
        };

        var history = new List<History>
        {
            new History
            {
                SecId = "AAPL",
                Date = DateTime.Now,
                Close = 160
            },
            new History
            {
                SecId = "AAPL",
                Date = DateTime.Now.AddDays(-1),
                Close = 170
            }
        };

        await _databaseService.DataContext.Transactions.AddRangeAsync(transactions, CancellationToken.None);
        await _databaseService.DataContext.History.AddRangeAsync(history, CancellationToken.None);
        await _databaseService.DataContext.SaveChangesAsync(CancellationToken.None);

        var securities = transactions.Select(t => new Security { SecId = t.Symbol }).ToList();

        // Act
        var result = await _repository.GetPortfolioCurrentCost(portfolioId1, securities, CancellationToken.None);

        // Assert
        // Только для портфеля 1: 10 * 160 = 1600
        result.Should().Be(1600);
    }

    [Fact]
    public async Task GetPortfolioCurrentCost_CancellationRequested_ThrowsOperationCanceledException()
    {
        // Arrange
        var portfolioId = 1;
        var cts = new CancellationTokenSource();
        cts.Cancel();

        // Act & Assert
        await FluentActions.Invoking(() =>
            _repository.GetPortfolioCurrentCost(portfolioId, [], cts.Token))
            .Should().ThrowAsync<OperationCanceledException>();
    }
}
using AutoMapper;
using InvestLens.Model;
using InvestLens.Model.Entities;
using InvestLens.Model.Enums;
using Microsoft.Extensions.Logging.Abstractions;

namespace InvestLens.Tests;

public class PortfolioProfilesTests
{
    private IMapper _mapper;

    public PortfolioProfilesTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<InvestLens.App.Profiles.PortfolioProfiles>();
        }, NullLoggerFactory.Instance);

        _mapper = config.CreateMapper();
    }

    [Theory]
    [InlineData(TransactionEvent.Buy)]
    [InlineData(TransactionEvent.Sell)]
    [InlineData(TransactionEvent.Dividend)]
    [InlineData(TransactionEvent.Stock_As_Dividend)]
    [InlineData(TransactionEvent.Split)]
    [InlineData(TransactionEvent.Spinoff)]
    [InlineData(TransactionEvent.Fee)]
    [InlineData(TransactionEvent.Amortisation)]
    [InlineData(TransactionEvent.Repayment)]
    [InlineData(TransactionEvent.Cash_In)]
    [InlineData(TransactionEvent.Cash_Out)]
    [InlineData(TransactionEvent.Cash_Gain)]
    [InlineData(TransactionEvent.Cash_Expense)]
    [InlineData(TransactionEvent.Cash_Convert)]
    [InlineData(TransactionEvent.Tax)]
    public void ShouldTransactionEventsConvertCorrect(TransactionEvent eventValue)
    {
        // Arrange
        var transaction = new Transaction()
        {
            Event = eventValue
        };

        // Act
        var operation = _mapper.Map<SecurityOperation>(transaction);

        // Assert
        Assert.NotNull(operation);
        Assert.Equal(eventValue, operation.OperationType);
    }
}
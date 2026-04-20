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
    [InlineData(TransactionEvents.Buy)]
    [InlineData(TransactionEvents.Sell)]
    [InlineData(TransactionEvents.Dividend)]
    [InlineData(TransactionEvents.Stock_As_Dividend)]
    [InlineData(TransactionEvents.Split)]
    [InlineData(TransactionEvents.Spinoff)]
    [InlineData(TransactionEvents.Fee)]
    [InlineData(TransactionEvents.Amortisation)]
    [InlineData(TransactionEvents.Repayment)]
    [InlineData(TransactionEvents.Cash_In)]
    [InlineData(TransactionEvents.Cash_Out)]
    [InlineData(TransactionEvents.Cash_Gain)]
    [InlineData(TransactionEvents.Cash_Expense)]
    [InlineData(TransactionEvents.Cash_Convert)]
    [InlineData(TransactionEvents.Tax)]
    public void ShouldTransactionEventsConvertCorrect(TransactionEvents eventValue)
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
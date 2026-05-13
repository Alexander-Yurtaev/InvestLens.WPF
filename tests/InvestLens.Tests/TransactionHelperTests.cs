using InvestLens.ViewModel.Helpers;
using System.Text;
using FluentAssertions;

namespace InvestLens.Tests;

public class TransactionHelperTests
{
    [Fact]
    public void ShouldCorrectConvertDecimalNumbers()
    {
        // Arrange
        var builder = new StringBuilder();
        builder.AppendLine("Event,Date,Symbol,Price,Quantity,Currency,FeeTax,Exchange,NKD,FeeCurrency,DoNotAdjustCash,Note");
        builder.AppendLine("BUY,2021-04-05 10:58:32,MTB,\"150,16666667\",\"0,12\",USD,\"0,02\",NYSE,\"\",\"\",\"False\",\"\"");
        
        // Act
        using var textReader = new StringReader(builder.ToString());
        var items = TransactionHelper.Convert(textReader);

        // Assert
        items.Count.Should().Be(1);
        var model = items.First();
        model.Should().NotBeNull();
        model.Price.Should().BeApproximately(150.16666667m, 0.00000001m);
    }
}
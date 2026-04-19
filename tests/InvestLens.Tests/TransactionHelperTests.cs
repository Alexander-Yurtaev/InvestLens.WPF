using Castle.Components.DictionaryAdapter.Xml;
using InvestLens.ViewModel.Helpers;
using Microsoft.Extensions.Logging;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        Assert.Single(items);
        var model = items.First();
        Assert.NotNull(model);
        Assert.Equal(150.16666667m, model.Price, 8);
    }
}
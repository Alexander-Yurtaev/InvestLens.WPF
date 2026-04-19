using InvestLens.ViewModel.Helpers;
using System.Text;

namespace InvestLens.Tests;

public class TransactionServiceTests
{
    private string[] lines = 
        [
            "Event,Date,Symbol,Price,Quantity,Currency,FeeTax,Exchange,NKD,FeeCurrency,DoNotAdjustCash,Note",
            "BUY,2021-04-05 10:58:32,MTB,\"150,16666667\",\"0,12\",USD,\"0,02\",NYSE,\"\",\"\",\"False\",\"\"",
            "BUY,2021-04-05 11:03:21,MCD,\"225,05\",\"1\",USD,\"0,13\",NYSE,\"\",\"\",\"False\",\"\"",
            "SPLIT,2021-04-12 03:00:00,OBLG,\"10\",\"0\",RUB,\"0\",MCX,\"0\",\"\",\"\",\"\"",
            "SELL,2021-05-05 10:32:11,FXTB,\"762\",\"4\",RUB,\"1,83\",MCX,\"\",\"\",\"False\",\"\"",
            "DIVIDEND,2021-05-14 00:00:00,T,\"0\",\"0,52\",USD,\"0,05\",NYSE,\"\",\"\",\"False\",\"\"",
            "DIVIDEND,2021-05-20 00:00:00,MTB,\"0\",\"0,18\",USD,\"0,02\",NYSE,\"\",\"\",\"True\",\"\"",
            "BUY,2021-06-03 10:40:13,FXIM,\"85,35\",\"6\",RUB,\"0,31\",MCX,\"\",\"\",\"False\",\"\""
        ];

    [Fact]
    public void ConvertShouldParseLinesAndReturnsListOfModels()
    {
        var builder = new StringBuilder();
        foreach (var line in lines)
        {
            builder.AppendLine(line);
        }

        using var textReader = new StringReader(builder.ToString());
        var items = TransactionHelper.Convert(textReader);
        
        Assert.NotNull(items);
        Assert.Equal(7, items.Count);
    }
}
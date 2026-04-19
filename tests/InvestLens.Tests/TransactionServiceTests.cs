using InvestLens.ViewModel.Helpers;

namespace InvestLens.Tests;

public class TransactionServiceTests
{
    private string[] lines = 
        [
            "Event,Date,Symbol,Price,Quantity,Currency,FeeTax,Exchange,NKD,FeeCurrency,DoNotAdjustCash,Note",
            "Cash_In,2020-02-01,RUB,1,100000,RUB,0,,,,,",
            "Cash_Convert,2020-02-01,USD,65000,1000,RUB,1,,,USD,,",
            "Buy,2020-03-06,AAPL,100,10,USD,1,,,,,\"Test\"",
            "Sell,2020-03-09,AAPL,110,5,USD,1,,,,,",
            "Buy,2020-10-30,SBER,224.6,10,RUB,30,MCX,,,,",
            "Dividend,2020-03-07,AAPL,0.76,7.6,USD,0.76,,,,,",
            "Fee,2020-04-01,,0,0,USD,10,,,,,"
        ];

    [Fact]
    public void ConvertShouldParseLinesAndReturnsListOfModels()
    {
        using var memory = new MemoryStream();
        using var streamWriter = new StreamWriter(memory);
        foreach (var line in lines)
        {
            streamWriter.WriteLine(line);
        }
        streamWriter.Flush();
        memory.Position = 0;

        using var streamReader = new StreamReader(memory);
        var models = TransactionHelper.Convert(streamReader);

        Assert.NotNull(models);
        Assert.Equal(7, models.Count);
    }
}
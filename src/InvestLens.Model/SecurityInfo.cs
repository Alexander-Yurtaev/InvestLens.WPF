namespace InvestLens.Model;

public class SecurityInfo : ICloneable
{
    public SecurityInfo(string secId, string name)
    {
        SecId = secId;
        Name = name;
    }

    public string SecId { get; }
    public string Name { get; }
    public decimal Count { get; set; }
    public decimal DividendCount { get; set; }
    public decimal AveragePrice => Count > 0 ? TotalPrice / Count : 0;
    public decimal CurrentPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal Profit { get; init; }

    public object Clone()
    {
        var clone = new SecurityInfo(SecId, Name)
        {
            Count = Count,
            DividendCount = DividendCount,
            CurrentPrice = CurrentPrice,
            TotalPrice = TotalPrice,
            Profit = Profit
        };
        return clone;
    }
}
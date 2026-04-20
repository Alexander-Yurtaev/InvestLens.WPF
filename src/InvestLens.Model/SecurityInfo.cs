namespace InvestLens.Model;

public class SecurityInfo
{
    public SecurityInfo(string secId, string name)
    {
        SecId = secId;
        Name = name;
    }

    public string SecId { get; }
    public string Name { get; }
    public decimal Count { get; init; }
    public decimal DividendCount { get; init; }
    public decimal AveragePrice { get; init; }
    public decimal CurrentPrice { get; init; }
    public decimal TotalPrice { get; init; }
    public decimal Profit { get; init; }
}
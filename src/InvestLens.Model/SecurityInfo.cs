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
    public int Count { get; init; }
    public double AveragePrice { get; init; }
    public double CurrentPrice { get; init; }
    public decimal TotalPrice { get; init; }
    public decimal Profit { get; init; }
}
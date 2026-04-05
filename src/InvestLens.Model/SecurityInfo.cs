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
    public double TotalPrice { get; init; }
    public double Profit { get; init; }
}
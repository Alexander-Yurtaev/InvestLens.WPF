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
    public decimal Quantity { get; set; }
    public decimal Dividends { get; set; }
    public decimal AveragePrice => Quantity > 0 ? TotalPrice / Quantity : 0;
    public decimal CurrentPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal Profit { get; init; }

    public object Clone()
    {
        var clone = new SecurityInfo(SecId, Name)
        {
            Quantity = Quantity,
            Dividends = Dividends,
            CurrentPrice = CurrentPrice,
            TotalPrice = TotalPrice,
            Profit = Profit
        };
        return clone;
    }
}
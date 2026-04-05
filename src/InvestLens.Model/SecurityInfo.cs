namespace InvestLens.Model;

public class SecurityInfo
{
    public string SecId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Count { get; set; }
    public decimal AveragePrice { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal Profit { get; set; }
}
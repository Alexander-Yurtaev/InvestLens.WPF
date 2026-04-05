namespace InvestLens.Model;

public class PortfolioDetail
{
    public string Title { get; set; } = string.Empty;
    public List<PortfolioStats> PortfolioStats { get; set; } = [];
    public List<SecurityInfo> Securities { get; set; } = [];
    public List<SecurityOperation> Operations { get; set; } = [];
}
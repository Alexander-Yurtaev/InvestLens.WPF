namespace InvestLens.Model;

public class PortfolioDetail
{
    public PortfolioDetail(string title)
    {
        Title = title;
        PortfolioStats = [];
        Securities = [];
        Operations = [];
    }

    public string Title { get; set; }
    public List<Stat> PortfolioStats { get; }
    public List<SecurityInfo> Securities { get; }
    public List<SecurityOperation> Operations { get; }
}
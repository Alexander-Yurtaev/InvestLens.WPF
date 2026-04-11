using InvestLens.Model.Enums;

namespace InvestLens.Model;

public class PortfolioDetail
{
    public PortfolioDetail(string title, PortfolioType portfolioType)
    {
        Title = title;
        PortfolioType = portfolioType;
        PortfolioStats = [];
        Securities = [];
        Operations = [];
    }

    public string Title { get; }
    public PortfolioType PortfolioType { get; set; }
    public List<Stat> PortfolioStats { get; }
    public List<SecurityInfo> Securities { get; }
    public List<SecurityOperation> Operations { get; }
}
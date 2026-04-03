using InvestLens.Model;
using InvestLens.Model.Enums;

namespace InvestLens.ViewModel;

public class PortfolioInfo
{
    public PortfolioInfo(string title, PortfolioType portfolioType, string refreshDate, List<PortfolioStats> portfolioStats)
    {
        Title = title;
        PortfolioType = portfolioType;
        RefreshDate = refreshDate;
        PortfolioStats = portfolioStats;
    }

    public string Title { get; }
    public PortfolioType PortfolioType { get; }
    public List<PortfolioStats> PortfolioStats { get; }
    public string RefreshDate { get; }
}


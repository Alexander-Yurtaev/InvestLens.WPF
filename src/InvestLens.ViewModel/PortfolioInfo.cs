using InvestLens.Model.Enums;

namespace InvestLens.ViewModel;

public class PortfolioInfo : BindableBase, IPortfolioInfo
{
    public PortfolioInfo(string title, PortfolioType portfolioType, string cost, string profitability, string amount, string refreshDate)
    {
        Title = title;
        PortfolioType = portfolioType;
        Cost = cost;
        Profitability = profitability;
        Amount = amount;
        RefreshDate = refreshDate;
    }

    public string Title { get; set; }
    public PortfolioType PortfolioType { get; set; }
    public string Cost { get; set; }
    public string Profitability { get; set; }
    public string Amount { get; set; }
    public string RefreshDate { get; set; }
}


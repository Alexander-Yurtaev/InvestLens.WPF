using InvestLens.Model.Entities;
using InvestLens.Model.Enums;

namespace InvestLens.Model;

public class PortfolioDetails
{
    public PortfolioDetails(int id, string title, PortfolioType portfolioType)
    {
        Id = id;
        Title = title;
        PortfolioType = portfolioType;
        Portfolios = [];
        PortfolioStats = [];
        Securities = [];
        Operations = [];
    }

    public int Id { get; }
    public string Title { get; }
    public string Description { get; init; } = string.Empty;
    public PortfolioType PortfolioType { get; set; }
    public List<int> Portfolios { get; }
    public List<Stat> PortfolioStats { get; }
    public List<SecurityInfo> Securities { get; }
    public List<SecurityOperation> Operations { get; }
}
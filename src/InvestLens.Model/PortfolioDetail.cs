using InvestLens.Model.Enums;

namespace InvestLens.Model;

public class PortfolioDetail
{
    public PortfolioDetail(int id, string title, PortfolioType portfolioType)
    {
        Id = id;
        Title = title;
        PortfolioType = portfolioType;
        PortfolioStats = [];
        Securities = [];
        Operations = [];
    }

    public int Id { get; }
    public string Title { get; }
    public string Description { get; init; }
    public PortfolioType PortfolioType { get; set; }
    public List<Stat> PortfolioStats { get; }
    public List<SecurityInfo> Securities { get; }
    public List<SecurityOperation> Operations { get; }
}
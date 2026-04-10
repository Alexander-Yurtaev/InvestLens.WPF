using InvestLens.Model.Enums;

namespace InvestLens.Model.Portfolio;

public abstract class BaseModel(string title, PortfolioType? portfolioType = PortfolioType.Invest) : LookupModel(title)
{
    protected PortfolioType _portfolioType = portfolioType ?? PortfolioType.Invest;
    
    public virtual PortfolioType PortfolioType => _portfolioType;
    public List<int> Portfolios { get; set; } = [];
}
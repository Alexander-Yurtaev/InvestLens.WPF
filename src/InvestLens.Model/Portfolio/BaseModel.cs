using InvestLens.Model.Enums;

namespace InvestLens.Model.Portfolio;

public abstract class BaseModel(int id, string title, PortfolioType portfolioType) : LookupModel(id, title)
{
    protected PortfolioType _portfolioType = portfolioType;
    
    public virtual PortfolioType PortfolioType => _portfolioType;
    public List<int> Portfolios { get; set; } = [];
}
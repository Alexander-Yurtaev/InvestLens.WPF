using InvestLens.Model.Enums;

namespace InvestLens.Model.Crud.Portfolio;

public abstract class BaseModel : Crud.BaseModel
{
    protected BaseModel(int id, string name, PortfolioType portfolioType) : base(id)
    {
        Name = name;
        PortfolioType = portfolioType;
    }

    protected BaseModel(string name, PortfolioType portfolioType)
    {
        Name = name;
        PortfolioType = portfolioType;
    }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public virtual PortfolioType PortfolioType { get; set; }
    public List<int> Portfolios { get; set; } = [];
}
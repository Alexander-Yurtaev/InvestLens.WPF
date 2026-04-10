using InvestLens.Model.Enums;

namespace InvestLens.Model.Portfolio;

public class CreateModel(string title, PortfolioType? portfolioType) : BaseModel(title, portfolioType)
{
    public void SetPortfolioType(PortfolioType value)
    {
        _portfolioType = value;
    }
}
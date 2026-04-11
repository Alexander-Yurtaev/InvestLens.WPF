using InvestLens.Model.Enums;

namespace InvestLens.Model.Portfolio;

public class CreateModel(string? title = null, PortfolioType? portfolioType = null) : BaseModel(title ?? "Новый портфел", portfolioType ?? PortfolioType.Invest)
{
    public void SetPortfolioType(PortfolioType value)
    {
        _portfolioType = value;
    }
}
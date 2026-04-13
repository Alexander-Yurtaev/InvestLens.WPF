using InvestLens.Model.Enums;

namespace InvestLens.Model.Portfolio;

public class CreateModel(int ownerId, string? title = null, PortfolioType? portfolioType = null) : BaseModel(title ?? "Новый портфель", portfolioType ?? PortfolioType.Invest)
{
    public int OwnerId { get; } = ownerId;

    public void SetPortfolioType(PortfolioType value)
    {
        _portfolioType = value;
    }
}
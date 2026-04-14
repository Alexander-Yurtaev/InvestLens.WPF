using InvestLens.Model.Enums;

namespace InvestLens.Model.Portfolio;

public class CreateModel(int ownerId) : BaseModel("Новый портфель", PortfolioType.Invest)
{
    public int OwnerId { get; } = ownerId;

    public void SetPortfolioType(PortfolioType value)
    {
        _portfolioType = value;
    }
}
using InvestLens.Model.Enums;

namespace InvestLens.Model.Crud.Portfolio;

public class CreateModel : BaseModel
{
    public CreateModel(int ownerId) : base("Новый портфель", PortfolioType.Invest)
    {
        OwnerId = ownerId;
    }

    public int OwnerId { get; }
}
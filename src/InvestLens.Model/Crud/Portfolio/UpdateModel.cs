using InvestLens.Model.Enums;

namespace InvestLens.Model.Crud.Portfolio;

public class UpdateModel : BaseModel
{
    public UpdateModel() : base("", PortfolioType.Invest)
    {
        
    }

    public UpdateModel(int id, string name, PortfolioType portfolioType) : base(id, name, portfolioType)
    {
    }
}
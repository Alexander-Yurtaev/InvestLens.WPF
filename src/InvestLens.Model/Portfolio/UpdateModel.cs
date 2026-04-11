using InvestLens.Model.Enums;

namespace InvestLens.Model.Portfolio;

public class UpdateModel : BaseModel
{
    public UpdateModel() : base("Ошибка", PortfolioType.Invest)
    {
        
    }

    public UpdateModel(string title, PortfolioType portfolioType) : base(title, portfolioType)
    {
        
    }
}
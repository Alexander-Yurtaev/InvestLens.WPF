using InvestLens.Model.Enums;

namespace InvestLens.Model.Portfolio;

public class UpdateModel : BaseModel
{
    public UpdateModel() : base(0, "Ошибка", PortfolioType.Invest)
    {
        
    }

    public UpdateModel(int id, string title, PortfolioType portfolioType) : base(id, title, portfolioType)
    {
        
    }
}
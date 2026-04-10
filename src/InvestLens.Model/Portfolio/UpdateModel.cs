using InvestLens.Model.Enums;

namespace InvestLens.Model.Portfolio;

public class UpdateModel(string title, PortfolioType? portfolioType) : BaseModel(title, portfolioType)
{

}
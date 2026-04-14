using InvestLens.Model.Enums;

namespace InvestLens.Model.NavigationTree;

public class PortfolioNavigationTreeModel(int id, PortfolioType portfolioType) : BaseNavigationTreeModel
{
    public int Id { get; } = id;
    public PortfolioType PortfolioType { get; set; } = portfolioType;
}
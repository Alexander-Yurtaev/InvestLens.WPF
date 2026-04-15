using InvestLens.Model.Enums;

namespace InvestLens.Model.NavigationTree;

public class PortfolioNavigationTreeModel(int id, string icon, string title,  PortfolioType portfolioType) 
    : BaseNavigationTreeModel(icon, title)
{
    public int Id { get; } = id;
    public PortfolioType PortfolioType { get; set; } = portfolioType;
}
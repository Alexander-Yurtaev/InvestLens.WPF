namespace InvestLens.Model.NavigationTree;

public class PortfolioNavigationTreeModel(int id) : BaseNavigationTreeModel
{
    public int Id { get; } = id;
}
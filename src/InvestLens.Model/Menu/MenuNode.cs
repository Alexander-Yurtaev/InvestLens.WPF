namespace InvestLens.Model.Menu;

public class MenuNode(string icon, string header, List<MenuNode>? children = null) : MenuItemWithHeaderModel(icon, header)
{
    public List<MenuNode> Children { get; } = children ?? [];
    public string Title { get; init; }
    public string Description { get; init; }
}
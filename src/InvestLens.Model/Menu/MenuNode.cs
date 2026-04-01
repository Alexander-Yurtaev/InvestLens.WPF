using InvestLens.Model.Enums;

namespace InvestLens.Model.Menu;

public class MenuNode(NodeTypes nodeType, string icon, string header, List<MenuNode>? children = null) : MenuItemWithHeaderModel(icon, header)
{
    public List<MenuNode> Children { get; } = children ?? [];
    public NodeTypes NodeType { get; init; } = nodeType;
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}
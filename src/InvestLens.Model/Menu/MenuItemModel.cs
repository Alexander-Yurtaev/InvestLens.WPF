using InvestLens.Model.Enums;

namespace InvestLens.Model.Menu;

public class MenuItemModel(NodeType nodeType, string icon, string header, List<MenuItemModel>? children = null)
{
    public NodeType NodeType { get; } = nodeType;
    public string Icon { get; } = icon;
    public string Header { get; } = header;
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public List<MenuItemModel> Children { get; } = children ?? [];
}
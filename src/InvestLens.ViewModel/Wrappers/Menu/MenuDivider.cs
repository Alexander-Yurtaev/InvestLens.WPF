using InvestLens.Model.Menu;

namespace InvestLens.ViewModel.Wrappers.Menu;

public class MenuDivider : IMenuNode
{
    public List<MenuItemWrapper> Children { get; } = [];
}
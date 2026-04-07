using InvestLens.Model;
using InvestLens.Model.Enums;
using InvestLens.Model.Menu;

namespace InvestLens.ViewModel.Services;

public interface IDohodService
{
    List<Card> Cards { get; }
    List<MenuItemModel> GetDohodBondsMenuItems();
    DohodBonds GetBonds(NodeType nodeType);
}
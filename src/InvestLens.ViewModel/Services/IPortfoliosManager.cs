using InvestLens.Model;
using InvestLens.Model.Enums;
using InvestLens.Model.Menu;

namespace InvestLens.ViewModel.Services;

public interface IPortfoliosManager
{
    List<Card> Cards { get; }
    List<MenuItemModel> GetPortfoliosMenuItems();
    PortfolioDetail GetPortfolio(NodeType nodeType);
}
using InvestLens.Model;
using InvestLens.Model.Enums;
using InvestLens.Model.Menu;

namespace InvestLens.ViewModel.Services;

public interface IPortfoliosManager
{
    List<Card> Cards { get; }
    List<MenuNode> GetPortfoliosMenuItems();
    PortfolioDetail GetPortfolio(NodeType nodeType);
}
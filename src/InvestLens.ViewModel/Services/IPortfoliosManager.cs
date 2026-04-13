using InvestLens.Model;
using InvestLens.ViewModel.NavigationTree;

namespace InvestLens.ViewModel.Services;

public interface IPortfoliosManager
{
    List<Card> Cards { get; }
    List<INavigationTreeItem> GetPortfoliosMenuItems();
    PortfolioDetail GetPortfolio(int id);
    List<Model.Portfolio.LookupModel> GetLookupModels();
}
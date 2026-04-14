using InvestLens.Model;
using InvestLens.ViewModel.NavigationTree;

namespace InvestLens.ViewModel.Services;

public interface IPortfoliosManager
{
    List<Card> Cards { get; }
    Task<List<INavigationTreeItem>> GetPortfoliosMenuItems(int userId);
    Task<PortfolioDetail?> GetPortfolio(int id);
    List<Model.Portfolio.LookupModel> GetLookupModels();
}
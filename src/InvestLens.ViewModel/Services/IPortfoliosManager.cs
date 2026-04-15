using InvestLens.Model;
using InvestLens.ViewModel.NavigationTree;

namespace InvestLens.ViewModel.Services;

public interface IPortfoliosManager
{
    List<Card> Cards { get; }
    Task<List<INavigationTreeItem>> GetPortfoliosMenuItems(int userId);
    Task<PortfolioDetail?> GetPortfolio(int id);
    Task<List<Model.Portfolio.LookupModel>> GetLookupModels(int ownerId, int? portfolioId = null);
}
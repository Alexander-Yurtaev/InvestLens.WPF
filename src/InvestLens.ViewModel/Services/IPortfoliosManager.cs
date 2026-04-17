using InvestLens.Model;
using InvestLens.Model.Crud.Portfolio;
using InvestLens.ViewModel.NavigationTree;

namespace InvestLens.ViewModel.Services;

public interface IPortfoliosManager
{
    List<Card> Cards { get; }
    List<INavigationTreeItem> GetPortfoliosMenuItems(int userId);
    Task<PortfolioDetail?> GetPortfolio(int id);
    Task<List<LookupModel>> GetLookupModels(int ownerId, int? portfolioId = null);
    Task<bool> CheckNameUniqueAsync(int portfolioId, int ownerId, string name);
    Task Create(CreateModel model);
    Task Delete(int id);
    Task Update(UpdateModel model);
}
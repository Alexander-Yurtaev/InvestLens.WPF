using InvestLens.Model;
using InvestLens.Model.Crud.Portfolio;
using InvestLens.Model.Entities;
using InvestLens.ViewModel.NavigationTree;

namespace InvestLens.ViewModel.Services;

public interface IPortfoliosManager
{
    List<Card> Cards { get; }
    List<INavigationTreeItem> GetPortfoliosMenuItems(int userId);
    Task<PortfolioDetails?> GetPortfolioDetiails(int id);
    List<LookupModel> GetLookupModels(int ownerId, int? portfolioId = null);
    Task<bool> CheckNameUniqueAsync(int portfolioId, int ownerId, string name);
    Task Create(CreateModel model);
    Task<bool> Delete(int id);
    Task Update(UpdateModel model);
    Task<int> Merge(List<Transaction> transactions);
    Task<int> Recreate(List<Transaction> transactions);
    Task ReloadPortfolio(int id);
}
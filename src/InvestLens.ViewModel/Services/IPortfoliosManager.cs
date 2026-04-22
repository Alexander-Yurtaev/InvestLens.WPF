using InvestLens.Model;
using InvestLens.Model.Crud.Portfolio;
using InvestLens.Model.Crud.Transaction;
using InvestLens.Model.Enums;
using InvestLens.ViewModel.NavigationTree;

namespace InvestLens.ViewModel.Services;

public interface IPortfoliosManager
{
    List<Card> Cards { get; }
    List<INavigationTreeItem> GetPortfoliosMenuItems();
    Task<PortfolioDetails?> GetPortfolioDetiails(int id);
    List<LookupModel> GetLookupModels(int ownerId, int? portfolioId = null);
    bool CheckNameUnique(int portfolioId, int ownerId, string name);
    Task Create(CreateModel model);
    Task<bool> Delete(int id);
    Task Update(UpdateModel model);
    Task<int> Merge(List<TransactionModel> transactions);
    Task<int> Recreate(List<TransactionModel> transactions);
    Task ReloadPortfolio(int id);

    List<PortfolioModel> GetAllPortfolios(PortfolioType portfolioType);

    Task<List<TransactionModel>> GetLastTtransactions(int count);
}